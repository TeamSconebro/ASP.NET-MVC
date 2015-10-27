using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoContest.Web.Models.BindingModel
{
    public class ImageBindingModelUserInput
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Base64Data { get; set; }
    }
}