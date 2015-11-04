namespace PhotoContest.Web.Models.ViewModels
{
    public class ContestPictureViewModel
    {
        public string Title { get; set; }

        public string ImageBase64Data { get; set; }

        public string OwnerId { get; set; }

        public int ContestId { get; set; }
    }
}