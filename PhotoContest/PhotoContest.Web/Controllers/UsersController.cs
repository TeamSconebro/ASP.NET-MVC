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
            return View(/*user*/);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,ImageBase64Data,ImageUrl,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] User user)
        {
            // TODO: Edit profile details. On success redirects to action "Index". On error - reload the "Edit" page

            if (ModelState.IsValid)
            {
                //this.Data.Users.Update(user);
                //this.Data.Entry(user).State = EntityState.Modified;
               // db.SaveChanges();
                //return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}
