using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata.Models
{
    public partial class Review
    {
        public Review()
        {
            PublishStates = new HashSet<PublishState>();
        }

        public int Id { get; set; }
        public int RatingValue { get; set; }
        public string? Comment { get; set; }
        public DateTime PublishDate { get; set; }
        public int Votes { get; set; }
        public bool? ImadeThis { get; set; }
        public bool? IhaveAquestion { get; set; }

        public virtual ICollection<PublishState> PublishStates { get; set; }
    }
}
