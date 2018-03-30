using System;
namespace SmartMinyanServer.Models
{
    public class Minyan
    {
        public DateTime CreationTime { get; set; }
        public DateTime MinyanTime {get; set;}
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public User Creator { get; set; }
        public string[] Comments { get; set; }
        public User[] Commitments { get; set; }
    }
}
