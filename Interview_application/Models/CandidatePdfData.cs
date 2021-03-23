using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interview_application.Models
{
    public class CandidatePdfData
    {
        public virtual int Id { get; set; }
        public virtual Candidate CandidateId { get; set; }
        public virtual String PdfName { get; set; }
        public virtual String PdfData { get; set; }
    }
}