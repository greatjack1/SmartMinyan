using System;
namespace SmartMinyanServer.Models
{
    public class Comment
    {
        public int Id { get; set; }
        //The username represented in the User class
        public String FullName { get; set; }
        public String CommentText { get; set; }
    }
}
