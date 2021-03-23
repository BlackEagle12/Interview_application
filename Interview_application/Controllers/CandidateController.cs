using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Connectivity;
using Interview_application.Models;


namespace Interview_application.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CandidateController : ApiController
    {
        [HttpGet]
        public IList GetAll()
        {
            var SessionFactory = DBconnection.GetSessionFactory();

            IList Candidates;

            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    Candidates =
                        session.CreateCriteria<Candidate>().List();
                    trans.Commit();
                }
            }
            return Candidates;
        }

        [Route("api/Candidate/GetCandidateByEmail/{Email}")]
        [HttpGet]
        public Candidate GetCandidateByEmail(string Email)
        {
            var SessionFactory = DBconnection.GetSessionFactory();
            Candidate candidate;
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                     candidate = session.Query<Candidate>()
                        .FirstOrDefault(x => x.Email == Email);
                }
            }
            return candidate;
        }

        //attribute routing
        [HttpGet]
        public Candidate GetCandidateById(int id)
        {
            Candidate Candidate;

            if (id == 0)
            {
                Candidate = new Candidate();
            }
            else
            {
                var SessionFactory = DBconnection.GetSessionFactory();

                using (var session = SessionFactory.OpenSession())
                {
                    using (var trans = session.BeginTransaction())
                    {
                        Candidate =
                            session.Get<Candidate>(id);
                        trans.Commit();
                    }
                }
            }

            return Candidate;
        }

        [HttpPost]
        public Uri AddCandidate([FromBody] Candidate candidate)
        {
            var existingCandidate = GetCandidateByEmail(candidate.Email);
            var SessionFactory = DBconnection.GetSessionFactory();
            if (existingCandidate == null)
            {
                using (var session = SessionFactory.OpenSession())
                {
                    using (var trans = session.BeginTransaction())
                    {
                        candidate.DateOfApplication = DateTime.Now;
                        session.Save(candidate);
                        trans.Commit();
                    }
                }
            }
            else
            {
                return UpdateCandidateById(existingCandidate.Id, candidate);
            }
            
            return new Uri($"https://localhost:44313/api/Candidate/{candidate.Id}/");
        }

        //[HttpPost]
        //public async Task<HttpResponseMessage> AddCandidateResume()
        //{
        //    if (!Request.Content.IsMimeMultipartContent()){
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //    }

        //    var SessionFactory = DBconnection.GetSessionFactory();

        //    string root = HttpContext.Current.Server.MapPath("~/App_Data");
        //    var provider = new MultipartFormDataStreamProvider(root);

        //    try
        //    {
        //        await Request.Content.ReadAsMultipartAsync(provider);

        //        // Show all the key-value pairs.
        //        foreach (var fileData in provider.FileData)
        //        {
        //            String fileName = fileData.Headers.ContentDisposition.FileName;
        //            if (String.IsNullOrEmpty(fileName))
        //            {
        //                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formated");
        //            }
        //            if(fileName.StartsWith("\"") && fileName.EndsWith("\""))
        //            {
        //                fileName = fileName.Trim('"');
        //            }
        //            if(fileName.Contains(@"/") || fileName.Contains(@"\"))
        //            {
        //                fileName = Path.GetFileName(fileName);
        //            }

        //            using (var ms = new MemoryStream())
        //            {
        //                var diskFile = new FileStream(fileData.LocalFileName, FileMode.Open);

        //                await diskFile.CopyToAsync(ms);

        //                var fileByteArray = ms.ToArray();

        //                var fileBase64String = Convert.ToBase64String(fileByteArray);
        //                //byte[] newByteFile = Convert.FromBase64String(Base64String);
        //                return Request.CreateResponse(HttpStatusCode.OK, fileBase64String);
        //            }
        //        }
        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch (System.Exception e)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotAcceptable, e.Message);
        //        //return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
        //    }
        //}


        [HttpPut]
        public Uri UpdateCandidateById(int id, [FromBody] Candidate updatecandidate)
        {
            var SessionFactory = DBconnection.GetSessionFactory();

            Candidate candidate;

            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    candidate =
                        session.Get<Candidate>(id);

                    candidate.DateOfApplication = DateTime.Now;
                    candidate.FName = updatecandidate.FName;
                    candidate.LName = updatecandidate.LName;
                    candidate.Gender = updatecandidate.Gender;
                    candidate.Address = updatecandidate.Address;
                    candidate.City = updatecandidate.City;
                    candidate.State = updatecandidate.State;
                    candidate.Country = updatecandidate.Country;
                    candidate.Qualification = updatecandidate.Qualification;
                    candidate.Percentage = updatecandidate.Percentage;
                    candidate.ApplyingFor = updatecandidate.ApplyingFor;
                    candidate.ContactNo = updatecandidate.ContactNo;
                    candidate.Email = updatecandidate.Email;
                    candidate.SkypeId = updatecandidate.SkypeId;
                    candidate.PrefferedModeOfInterview = updatecandidate.PrefferedModeOfInterview;
                    candidate.IsShortListed = updatecandidate.IsShortListed;
                    candidate.IsAdmin = updatecandidate.IsAdmin;
                    session.Save(candidate);
                    trans.Commit();
                }
            }
            return new Uri($"https://localhost:44313/api/Candidate/{id}/");
        }

        [HttpDelete]
        public Candidate DeleteCandidate(int id)
        {
            Candidate Candidate = GetCandidateById(id);

            var SessionFactory = DBconnection.GetSessionFactory();

            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.Delete(Candidate);
                    trans.Commit();
                }
            }
            return Candidate;
        }
    }
}
