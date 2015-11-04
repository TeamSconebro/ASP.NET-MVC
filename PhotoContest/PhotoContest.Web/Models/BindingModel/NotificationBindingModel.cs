namespace PhotoContest.Web.Models.BindingModel
{
    using System.ComponentModel.DataAnnotations;
    using PhotoContest.Models.Enumerations;

    public class NotificationBindingModel
    {
        [Required]
        public string NotificationContent { get; set; }

        public NotificationType Type { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}