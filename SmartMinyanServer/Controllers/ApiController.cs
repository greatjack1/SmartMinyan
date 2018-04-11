using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartMinyanServer.Models;
using SmartMinyanServer.AuthFilters;
using SmartMinyanServer.Models.ControllerModels;

namespace SmartMinyanServer.Controllers
{
    public class ApiController : Controller
    {
        private IRepository mRepo;

        public ApiController(IRepository repository){
            mRepo = repository;
        }
        // GET all minyanim in the database
        //Use the auth filter to prevent unautherized requests
        [TypeFilter(typeof(AuthFilter))]
        [HttpGet]
        public JsonResult GetAllMinyanim()
        {
            return Json(mRepo.getMinyanim());
        }

        // GET nearby minyanim
        [TypeFilter(typeof(AuthFilter))]
        [HttpGet]
        public JsonResult GetNearbyMinyanim(double latitude, double longitude,double degreesAround)
        {
            return Json(mRepo.getNearbyMinyan(latitude,longitude,degreesAround));
        }

        // POST a new minyan
        [TypeFilter(typeof(AuthFilter))]
        [HttpPost]
        public JsonResult PostMinyan([FromBody] Minyan minyan)
        {
            try
            {
                mRepo.AddMinyan(minyan);
                return Json("Success");
            } catch(Exception ex){
                Console.WriteLine("Error when adding minyan. MSG: " + ex.Message);
                return Json("Error");
            }
        }
        //Login post method, send in a email address and password and get back the guid token for that user
        //No Auth since we are just logging in
        [HttpPost]
        public JsonResult PostLogin([FromBody] PostLogin credentials)
        { 
            //if credentials are valid then return the guid
            try{
                var user = mRepo.getUsers().First((n) => ((n.EmailAddress == credentials.EmailAddress) && n.PassWord == credentials.PassWord));
                return Json(user);
            } catch(Exception ex){
                Console.WriteLine("Error on PostLogin. MSG: " + ex.Message);
                //user not found, return a blank user
                return Json(new User());
            }
        }
        // POST a new User
        // There is no auth filter here since we have to be able to create a user to get an authentication token
        [HttpPost]
        public JsonResult PostUser([FromBody] User user)
        {
            try
            {
                bool success = mRepo.AddUser(user);
                if (success)
                {
                    return Json("Success");
                }
                else
                {
                    return Json("Error");
                }
            } catch(Exception ex){
                Console.WriteLine("Error when adding user. MSG:" +ex.Message);
                return Json("Error");
            }
        }

        // DELETE Minyan
        [TypeFilter(typeof(AuthFilter))]
        [HttpDelete]
        public void DeleteMinyan(int id)
        {
            mRepo.DeleteMinyan(id);
        }
        // DELETE User
        [TypeFilter(typeof(AuthFilter))]
        [HttpDelete]
        public void DeleteUser(int id)
        {
            mRepo.DeleteUser(id);
        }
        //UPDATE minyan
        [TypeFilter(typeof(AuthFilter))]
        [HttpPatch]
        public void PatchMinyan([FromBody] Minyan minyan) {
            mRepo.UpdateMinyan(minyan);
        }

        [TypeFilter(typeof(AuthFilter))]
        [HttpPatch]
        public void PatchUser([FromBody] User user)
        {
            mRepo.UpdateUser(user);
        }
        [TypeFilter(typeof(AuthFilter))]
        [HttpPost]
        public String PostComment([FromBody] PostComment comment)
        {
            //add the comment to the minyan
            try
            {
                var minyan = mRepo.getMinyanim().First((n) => n.Guid == comment.MinyanGuid);
                minyan.Comments.Add(comment);
                mRepo.UpdateMinyan(minyan);
                return "Success";
            } catch (Exception ex){
                Console.WriteLine("Error when posting comment, please ensure minyan guid exists. Error msg: " + ex.Message);
                return "Error";
            }
        }

    }
}
