using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoContest.Web.Models.BindingModel
{
    public class ContestDetailsForUploadImage
    {
        public int ContestId { get; set; }

        public string ContestTitle { get; set; }
    }
}