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
     
        // DELETE Minyan
        [HttpDelete]
        public void DeleteMinyan(int id)
        {
            mRepo.DeleteMinyan(id);
        }
        // DELETE User
        [HttpDelete]
        public void DeleteUser(int id)
        {
            mRepo.DeleteUser(id);
        }
        //UPDATE minyan
        [HttpPatch]
        public void PatchMinyan([FromBody] Minyan minyan) {
            mRepo.UpdateMinyan(minyan);
        }

        [HttpPatch]
        public void PatchUser([FromBody] User user)
        {
            mRepo.UpdateUser(user);
        }

    }
}
