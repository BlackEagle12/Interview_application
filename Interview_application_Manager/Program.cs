using Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview_application_Manager
{
    public class Program
    {
        static void Main(string[] args)
        {
        }

        public IList GetAllEmployees()
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

        [HttpGet]
        public Candidate GetById(int id)
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
            var SessionFactory = DBconnection.GetSessionFactory();

            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.Save(candidate);
                    trans.Commit();
                }
            }

            return new Uri($"https://localhost:44313/api/Candidate/{candidate.Id}");
        }

        [HttpPut]
        public Uri UpdateCandidate(int id, [FromBody] Candidate updatecandidate)
        {
            var SessionFactory = DBconnection.GetSessionFactory();

            Candidate candidate;

            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    candidate =
                        session.Get<Candidate>(id);

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

                    trans.Commit();
                }
            }
            return new Uri($"https://localhost:44313/api/Candidate/{candidate.Id}");
        }

        [HttpDelete]
        public Candidate DeleteCandidate(int id)
        {
            Candidate Candidate;

            var SessionFactory = DBconnection.GetSessionFactory();

            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    Candidate =
                        session.Get<Candidate>(id);
                    session.Delete(Candidate);
                    trans.Commit();
                }
            }
            return Candidate;
        }
    }
}
