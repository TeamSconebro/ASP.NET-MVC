using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoContest.Web.Models.ViewModels
{
    public class ListPictureViewModel
    {
        public string Title { get; set; }

        public string ImageBase64Data { get; set; }

        public string OwnerUserName { get; set; }
    }
}