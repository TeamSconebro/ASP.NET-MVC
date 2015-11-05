using System.Web;

namespace PhotoContest.Web.Models.BindingModel
{
    using System.ComponentModel.DataAnnotations;

    public class ImageBindingModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public HttpPostedFileBase Photo { get; set; }

        [Required]
        public int ContestId { get; set; }
    }
}