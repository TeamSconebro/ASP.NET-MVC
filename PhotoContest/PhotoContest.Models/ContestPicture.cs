namespace PhotoContest.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ContestPicture
    {
        private ICollection<Vote> votes;

        public ContestPicture()
        {
            this.votes = new HashSet<Vote>();
        } 

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Base64Data { get; set; }
        
        //public int VotesCount { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public int ContestId { get; set; }

        public virtual Contest Contest { get; set; }

        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }
    }
}
