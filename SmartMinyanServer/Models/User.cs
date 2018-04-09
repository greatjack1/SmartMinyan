using System;
namespace SmartMinyanServer.Models
{
    public class User
    {
        public int Id { get; set; }
        //special string assigned to each user
        public string Guid { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string PassWord { get; set; }
    }
}
