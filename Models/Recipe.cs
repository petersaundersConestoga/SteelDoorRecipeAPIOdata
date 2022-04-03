using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata
{
    public partial class Recipe
    {
        public Recipe()
        {
            DietLists = new HashSet<DietList>();
            ImageRecipes = new HashSet<ImageRecipe>();
            IngredientLists = new HashSet<IngredientList>();
            Instructions = new HashSet<Instruction>();
            PersonReviews = new HashSet<PersonReview>();
            PublishStates = new HashSet<PublishState>();
            SeasonLists = new HashSet<SeasonList>();
            Timings = new HashSet<Timing>();
        }

        public int Id { get; set; }
        public int PersonId { get; set; }
        public int CuisineId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public int ServingCount { get; set; }
        public string Story { get; set; } = null!;
        public int Difficulty { get; set; }

        public virtual Cuisine Cuisine { get; set; } = null!;
        public virtual ICollection<DietList> DietLists { get; set; }
        public virtual ICollection<ImageRecipe> ImageRecipes { get; set; }
        public virtual ICollection<IngredientList> IngredientLists { get; set; }
        public virtual ICollection<Instruction> Instructions { get; set; }
        public virtual ICollection<PersonReview> PersonReviews { get; set; }
        public virtual ICollection<PublishState> PublishStates { get; set; }
        public virtual ICollection<SeasonList> SeasonLists { get; set; }
        public virtual ICollection<Timing> Timings { get; set; }
    }
}
