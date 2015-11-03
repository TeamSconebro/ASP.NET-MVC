

using System.Collections.Generic;
using AutoMapper;

namespace PhotoContest.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.Ajax.Utilities;
    using Microsoft.AspNet.Identity;
    using Data.UnitsOfWork;
    using Models.ViewModels;
    [Authorize]
    public class UsersController : BaseController
    {
        public UsersController(IPhotoContestData data) : base(data)
        {

        }

        public ActionResult GetUsers(string search)
        {
                var users = this.Data.Users.All()
                    .Where(c => c.UserName.StartsWith(search))
                    .OrderBy(u => u.UserName)
                    .Select(u => u.UserName)
                    .Take(5)
                    .ToList();
                return this.Json(users, JsonRequestBehavior.AllowGet);
        }

        // GET: Users
        [ActionName("Profile")]
        public ActionResult AccountProfile(UserViewModel user)
        {
            var currentUserId = this.User.Identity.GetUserId();

            var currentUser = this.Data.Users.Find(currentUserId);
            
            if (currentUser == null)
            {
                return this.RedirectToAction("Index");

            }
            user.Email = currentUser.Email;
            user.Coints = currentUser.Coints;
            user.FirstName = currentUser.FirstName;
            user.LastName = currentUser.LastName;
            user.UserName = currentUser.UserName;
            user.PhoneNumber = currentUser.PhoneNumber;
            var contestCollection = this.Data.Contests.All().Where(c => c.OwnerId == currentUserId).OrderByDescending(c => c.CreatedOn);
            user.ContestViewModels = Mapper.Map<IEnumerable<ContestUserProfileViewModel>>(contestCollection);

            var contestPictures = this.Data.ContestPictures.All().Where(c => c.OwnerId == currentUserId);

            user.ContestPictureViewModels = Mapper.Map<IEnumerable<ContestPictureUserProfileViewModel>>(contestPictures);
            

            return View(user);
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
            return RedirectToAction("Profile", "Users");
        }

        public ActionResult Notifications()
        {

            return this.View();
        }

       
    }
}
