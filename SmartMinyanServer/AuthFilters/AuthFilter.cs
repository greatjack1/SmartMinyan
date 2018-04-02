using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartMinyanServer.Models;

namespace SmartMinyanServer.AuthFilters
{
    public class AuthFilter : ResultFilterAttribute
    {
        public IRepository mRepo;
        public AuthFilter(IRepository repo){
            mRepo = repo;
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if(context.HttpContext.Request.Headers.ContainsKey("auth")){
                string guid = context.HttpContext.Request.Headers["auth"];
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
                context.HttpContext.Response.Body.
            }

        }
    }
}
