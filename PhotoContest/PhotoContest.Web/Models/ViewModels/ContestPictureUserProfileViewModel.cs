using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoContest.Web.Models.ViewModels
{
    public class ContestPictureUserProfileViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Base64Data { get; set; }

        public int ContestId { get; set; }

        public string ConstestTitle { get; set; }
    }
}