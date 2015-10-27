using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PhotoContest.Data;
using PhotoContest.Data.UnitsOfWork;
using PhotoContest.Models;
using PhotoContest.Web.Models.ViewModels;

namespace PhotoContest.Web.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        public UsersController(IPhotoContestData data) : base(data)
        {

        }

        public ActionResult Index()
        {
            // TODO: Return empty view, which body contains partial views for listing active and inctive contests
            return this.View();
        }

        // GET: Users
        public ActionResult AccountProfile()
        {
            // TODO: Return account profile view, which keep all profile details
            //var currentUserId = this.User.Identity.GetUserId();

            //var currentUser = this.Data.Users.Find(currentUserId);
            //if (currentUser == null)
            //{
            //    //return this.RedirectToAction("")

            //}


            //return View(this.Data.Users.All());

            return this.View();
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            // TODO: Load view for edditing profile details
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //User user = this.Data.Users.Find(id);
            //if (user == null)
            //{
            //    return HttpNotFound();
            //}
            return View( /*user*/);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(UserEditProfileViewModel userprofile)
        {
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;

                // Fetch the userprofile
                var user = this.Data.Users.All().FirstOrDefault(u => u.UserName.Equals(username));

                // Construct the viewmodel

                user.FirstName = userprofile.FirstName;
                user.LastName = userprofile.LastName;
                user.Email = userprofile.Email;
                user.PhoneNumber = userprofile.PhoneNumber;


                // this.Data.Entry(user).State = EntityState.Modified;
                this.Data.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(userprofile);
        }
    }
}
