using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoContest.Web.Models.ViewModels
{
    public class ContestPictureViewModel
    {
        public string Title { get; set; }

        public string ImageBase64Data { get; set; }

        public int VotesCount { get; set; }

        public string OwnerId { get; set; }

        public int ContestId { get; set; }
    }
}