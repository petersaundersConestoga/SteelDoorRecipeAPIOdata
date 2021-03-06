using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata
{
    public partial class Review
    {
        public Review()
        {
            PersonReviews = new HashSet<PersonReview>();
            PublishStates = new HashSet<PublishState>();
        }

        public int Id { get; set; }
        public int RatingValue { get; set; }
        public string? Comment { get; set; }
        public DateTime PublishDate { get; set; }
        public int Votes { get; set; }
        public bool? ImadeThis { get; set; }
        public bool? IhaveAquestion { get; set; }

        public virtual ICollection<PersonReview> PersonReviews { get; set; }
        public virtual ICollection<PublishState> PublishStates { get; set; }
    }
}
