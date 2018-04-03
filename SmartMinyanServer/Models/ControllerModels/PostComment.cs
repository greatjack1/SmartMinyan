using System;
namespace SmartMinyanServer.Models.ControllerModels
{
    //adds on an extra minyan guid
    public class PostComment : Comment
    {
        //The minyan to add the comment to
        public String MinyanGuid { get; set; }
    }
}
