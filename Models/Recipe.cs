using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            DietLists = new HashSet<DietList>();
            Instructions = new HashSet<Instruction>();
            PublishStates = new HashSet<PublishState>();
            SeasonLists = new HashSet<SeasonList>();
            Timings = new HashSet<Timing>();
        }

        public int Id { get; set; }
        public int PersonId { get; set; }
        public int CuisineId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public int? ServingCount { get; set; }
        public string? Story { get; set; }
        public int? Difficulty { get; set; }

        public virtual Cuisine Cuisine { get; set; } = null!;
        public virtual Person Person { get; set; } = null!;
        public virtual ICollection<DietList> DietLists { get; set; }
        public virtual ICollection<Instruction> Instructions { get; set; }
        public virtual ICollection<PublishState> PublishStates { get; set; }
        public virtual ICollection<SeasonList> SeasonLists { get; set; }
        public virtual ICollection<Timing> Timings { get; set; }
    }
}
