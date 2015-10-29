using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
        public ActionResult AccountProfile(UserViewModel user)
        {
            // TODO: Return account profile view, which keep all profile details
            var currentUserId = this.User.Identity.GetUserId();

            var currentUser = this.Data.Users.Find(currentUserId);
            
            if (currentUser == null)
            {
                return this.RedirectToAction("Index");

            }
            user.Coints = currentUser.Coints;
            user.FirstName = currentUser.FirstName;
            user.LastName = currentUser.LastName;
            user.UserName = currentUser.UserName;
            user.ContestViewModels = this.Data.Contests.All().Where(c => c.OwnerId == currentUserId).OrderByDescending(c => c.CreatedOn);

            user.ContestPictureViewModels = this.Data.ContestPictures.All().Where(c => c.OwnerId == currentUserId);
            

            return View(user);

            //return this.View();
        }

        // GET: Users/Edit/username
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(string username)
        {
            // TODO: Load view for edditing profile details
            //if (username == null)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditProfileViewModel userprofile)
        {
            if (!ModelState.IsValid) return View(userprofile);
            var username = User.Identity.Name;

            // Fetch the userprofile
            var user = this.Data.Users.All().FirstOrDefault(u => u.UserName.Equals(username));
            // Construct the viewmodel
            if (user != null)
            {
                user.FirstName = userprofile.FirstName.IsNullOrWhiteSpace() ? user.FirstName : userprofile.FirstName;
                user.LastName = userprofile.LastName.IsNullOrWhiteSpace() ? user.LastName : userprofile.LastName;
                user.Email = userprofile.Email.IsNullOrWhiteSpace() ? user.Email : userprofile.Email;
                user.PhoneNumber = userprofile.PhoneNumber.IsNullOrWhiteSpace() ? user.PhoneNumber : userprofile.PhoneNumber;
            }
            this.Data.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}
