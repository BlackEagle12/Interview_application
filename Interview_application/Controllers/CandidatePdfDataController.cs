using Connectivity;
using Interview_application.Models;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using NHibernate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Interview_application.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CandidatePdfDataController : ApiController
    {
        ISessionFactory SessionFactory = DBconnection.GetSessionFactory();

        [HttpGet]
        public IList<CandidatePdfData> GetAll()
        {
            IList<CandidatePdfData> CandidatePdfs;

            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    CandidatePdfs =
                        session.CreateCriteria<CandidatePdfData>().List<CandidatePdfData>();
                    trans.Commit();
                }
            }
            return CandidatePdfs;
        }

        [HttpGet]
        public CandidatePdfData GetPdfByCandidateId(int id)
        {

            CandidatePdfData CandidatePdf;

            if (id == 0)
            {
                CandidatePdf = new CandidatePdfData();
            }
            else
            {
                using (var session = SessionFactory.OpenSession())
                {
                    using (var trans = session.BeginTransaction())
                    {
                        CandidatePdf =
                            session.Query<CandidatePdfData>().FirstOrDefault(x => x.CandidateId.Id == id);
                        trans.Commit();
                    }
                }
            }
            return CandidatePdf;
        }


        [Route("api/CandidatePdfData/FindTextSearchedPdf/{searchtext}/")]
        [HttpGet]
        public List<int> FindTextSearchedPdf(string searchtext)
        {
            IList<CandidatePdfData> AllPdfs;
            List<int> SearchedPdf = new List<int>();

            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    AllPdfs = GetAll();
                    foreach (var pdf in AllPdfs)
                    {
                        if (Serach(pdf.PdfData, searchtext)) SearchedPdf.Add(pdf.CandidateId.Id);
                    }
                    trans.Commit();
                }
            }
            return SearchedPdf;
        }

        private bool Serach(string pdfData, string searchtext)
        {
            byte[] bytes = Convert.FromBase64String(pdfData);
            PdfReader reader = new PdfReader(bytes);
            string text = string.Empty;
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                text += PdfTextExtractor.GetTextFromPage(reader, page);
            }
            reader.Close();

            if (text.Contains(searchtext)) return true;
            else return false;
        }

        [HttpPost]
        public Uri AddPdf([FromBody] CandidatePdfData candidatePdf)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    var ExsistingPdf = GetPdfByCandidateId(candidatePdf.CandidateId.Id);
                    if (ExsistingPdf == null)
                    {
                        session.Save(candidatePdf);
                        trans.Commit();
                        return new Uri($"https://localhost:44313/api/CandidatePdfData/{candidatePdf.CandidateId}");
                    }
                    else
                    {
                        return UpdatePdfByCandidateId(candidatePdf.CandidateId.Id, candidatePdf);
                    }
                }
            }
        }

        [HttpPut]
        public Uri UpdatePdfByCandidateId(int id, [FromBody] CandidatePdfData candidatePdf)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    var ExistingPdf = GetPdfByCandidateId(id);
                    ExistingPdf.CandidateId = candidatePdf.CandidateId;
                    ExistingPdf.PdfName = candidatePdf.PdfName;
                    ExistingPdf.PdfData = candidatePdf.PdfData;
                    session.Update(ExistingPdf);
                    trans.Commit();
                }
            }
            return new Uri($"https://localhost:44313/api/CandidatePdfData/{candidatePdf.CandidateId}");
        }

        [HttpDelete]
        public CandidatePdfData DeletePdfByCandidateId(int id)
        {
            var candidatePdfData = GetPdfByCandidateId(id);
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.Delete(candidatePdfData);
                    trans.Commit();
                }
            }
            return candidatePdfData;
        }
    }
}
