using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartMinyanServer.Models;
using System.Text;
using System.Diagnostics;

namespace SmartMinyanServer.AuthFilters
{
    /// <summary>
    /// This Authorization filter ensures that the request provides a valid user guid when accessing api methods that need authentication
    /// </summary>
    public class AuthFilter : ResultFilterAttribute
    {
        public IRepository mRepo;
        public AuthFilter(IRepository repo){
            mRepo = repo;
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if(context.HttpContext.Request.Headers.ContainsKey("Authorization")){
                string guid = context.HttpContext.Request.Headers["Authorization"];
                //ensure the request header auth contains a valid user guid
                try
                {
                    var withGuid = mRepo.getUsers().First((n) => n.Guid == guid);
                } catch (Exception ex){
                    Console.WriteLine("auth header does not exist, cancel the request and return a string saying invalid auth");
                    Console.WriteLine(ex.Message);
                    context.Cancel = true;
                }
            } else {
                //cancel since their is no auth header
                context.Cancel = true;
            }
            if(context.Cancel==true){
                //set the result property to show a message that we require auth
                string message = "Authentication is required to make this request";
                byte[] msgBytes = Encoding.ASCII.GetBytes(message);
                //write the response to the stream
                context.HttpContext.Response.Body.Write(msgBytes,0,msgBytes.Length); 
            }

        }
    }
}
