namespace PhotoContest.Web.Models.BindingModel
{
    using System.ComponentModel.DataAnnotations;

    public class ImageBindingModelUserInput
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Base64Data { get; set; }
    }
}