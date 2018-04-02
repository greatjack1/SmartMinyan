using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartMinyanServer.Models;
using SmartMinyanServer.AuthFilters;

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
        public JsonResult GetNearbyMinyanim(double latitude, double longitude,double milesNearby)
        {
            return Json(mRepo.getNearbyMinyan(latitude,longitude,milesNearby));
        }

        // POST a new minyan
        [TypeFilter(typeof(AuthFilter))]
        [HttpPost]
        public void PostMinyan([FromBody] Minyan minyan)
        {
            mRepo.AddMinyan(minyan);
        }

        // POST a new User
        // There is no auth filter here since we have to be able to create a user to get an authentication token
        [HttpPost]
        public JsonResult PostUser([FromBody] User user)
        {
         bool success =  mRepo.AddUser(user);
            if (success)
            {
                return Json("Success");
            }
            else {
                return Json("Error: User allready exists");
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

    }
}
