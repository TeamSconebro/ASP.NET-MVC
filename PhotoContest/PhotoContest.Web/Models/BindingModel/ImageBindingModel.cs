namespace PhotoContest.Web.Models.BindingModel
{
    using System.ComponentModel.DataAnnotations;

    public class ImageBindingModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Base64Data { get; set; }

        [Required]
        public int ContestId { get; set; }
    }
}