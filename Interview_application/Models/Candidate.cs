using System;

namespace Interview_application.Models
{
    public class Candidate
    {
        public virtual int Id { get; set; }
        public virtual String FName { get; set; }
        public virtual String LName { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual String Address { get; set; }
        public virtual String City { get; set; }    
        public virtual String State { get; set; }    
        public virtual String Country { get; set; }    
        public virtual String Qualification { get; set; }    
        public virtual int Percentage { get; set; }   
        public virtual ApplyingFor ApplyingFor { get; set; }
        public virtual String ContactNo { get; set; }
        public virtual String Email { get; set; }
        public virtual String SkypeId { get; set; }
        public virtual DateTime DateOfApplication { get; set; }
        public virtual PrefferedModeOfInterview PrefferedModeOfInterview { get; set; }
        public virtual bool IsShortListed { get; set; } = false;
        public virtual bool IsAdmin { get; set; } = false;
    }

    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3
    }

    public enum ApplyingFor
    {
        Dotnet_fullstack = 1,
        Front_end = 2,
        BackEnd = 3,
        Hr = 4,
        Hr_manager = 5,
    }

    public enum PrefferedModeOfInterview
    {
        Offline = 1,
        Online = 2
    }
}