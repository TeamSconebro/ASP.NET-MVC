using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoContest.Web.Models.ViewModels
{
    public class ContestViewModelActiveInactivePage
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public string OwnerId { get; set; }

        public string OwnerUsername { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}