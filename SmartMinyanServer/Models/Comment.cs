using System;
namespace SmartMinyanServer.Models
{
    public class Comment
    {
        public string Id { get; set; }
        //The username represented in the User class
        public String UserName { get; set; }
        public String CommentText { get; set; }
    }
}
