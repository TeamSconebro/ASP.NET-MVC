﻿namespace PhotoContest.Web.Models.ViewModels
{
    public class ListPictureViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Base64Data { get; set; }

        public string OwnerId { get; set; }

        public string OwnerUserName { get; set; }
    }
}