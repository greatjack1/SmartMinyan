﻿using System;
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
        //Login post method, send in a email address and password and get back the guid token for that user
        //No Auth since we are just logging in
        [HttpPost]
        public string PostLogin([FromBody] PostLogin credentials)
        { 
            //if credentials are valid then return the guid
            try{
                var user = mRepo.getUsers().First((n) => ((n.EmailAddress == credentials.EmailAddress) && n.PassWord == credentials.PassWord));
                return user.Guid;
            } catch(Exception ex){
                //user not found, return Error, no user found
                return "Error: No user found";
            }
        }
        // POST a new User
        // There is no auth filter here since we have to be able to create a user to get an authentication token
        [HttpPost]
        public String PostUser([FromBody] User user)
        {
         bool success =  mRepo.AddUser(user);
            if (success)
            {
                return "Success";
            }
            else {
                return "Error";
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
