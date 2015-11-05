using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using PhotoContest.Models;
using PhotoContest.Models.Enumerations;
using PhotoContest.Web.Models.ViewModels;

namespace PhotoContest.Web.Areas.Admin.Controllers
{
    public class UsersController : BaseAdminController
    {
        public ActionResult GetAllUsers()
        {
            var users = this.Data.
                Users.All().
                OrderByDescending(u=>u.UserName);
            var userMapped = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);
            return View(userMapped);
        }

        [HttpGet]
        public ActionResult EditUser(string username)
        {
            this.ViewBag.Title = username;

            var user = this.Data
                           .Users
                           .All()
                           .Where(u => u.UserName == username)
                           .ProjectTo<UserEditProfileViewModel>()
                           .FirstOrDefault();

            if (user == null)
            {
                return this.HttpNotFound();
            }

            return this.View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(UserEditProfileViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("EditUser", new {username = model.UserName});
            }

            var user = this.Data
                           .Users
                           .All()
                           .FirstOrDefault(u => u.UserName == model.UserName);

            if (user == null)
            {
                return this.HttpNotFound();
            }

            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;

            this.Data.SaveChanges();
            return this.RedirectToAction("Index", "Home");
        }
    }
    }
