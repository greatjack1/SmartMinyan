using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartMinyanServer.Models;

namespace SmartMinyanServer.Controllers
{
    public class ApiController : Controller
    {
        private IRepository mRepo;

        public ApiController(IRepository repository){
            mRepo = repository;
        }
        // GET all minyanim in the database
        [HttpGet]
        public JsonResult GetAllMinyanim()
        {
            return Json(mRepo.getMinyanim());
        }

        // GET nearby minyanim
        [HttpGet]
        public JsonResult GetNearbyMinyanim(double latitude, double longitude,double milesNearby)
        {
            return Json(mRepo.getNearbyMinyan(latitude,longitude,milesNearby));
        }

        // POST a new minyan
        [HttpPost]
        public void PostMinyan([FromBody] Minyan minyan)
        {
            mRepo.AddMinyan(minyan);
        }

        // POST a new User
        [HttpPost]
        public void PostUser([FromBody] User user)
        {
            mRepo.AddUser(user);
        }
     
        // DELETE api/values/5
        [HttpDelete]
        public void DeleteMinyan(int id)
        {
            
        }
    }
}
