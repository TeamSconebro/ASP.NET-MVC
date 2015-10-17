using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Models
{
    public class ContestPicture
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Base64Data { get; set; }

        public string ImageUrl { get; set; }

        public int VotesCount { get; set; }

        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public int ContestId { get; set; }

        public virtual Contest Contest { get; set; }

    }
}
