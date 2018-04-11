using System;
using System.Collections.Generic;

namespace SmartMinyanServer.Models
{
    public class Minyan
    {
        public DateTime CreationTime { get; set; }
        public DateTime MinyanTime {get; set;}
        public int Id { get; set; }
        //a special guid to represent each minyan
        public string Guid { get; set; }
        public string Teffilah { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public Boolean Deleted { get; set; }
        public string CreatorGUID { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        //The GUID of users that commited
        public List<String> Commitments { get; set; } = new List<String>();
    }
}
