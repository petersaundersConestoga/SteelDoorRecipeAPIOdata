using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata
{
    public partial class Cuisine
    {
        public Cuisine()
        {
            Recipes = new HashSet<Recipe>();
        }

        public int Id { get; set; }
        public string Region { get; set; } = null!;
        public string Country { get; set; } = null!;

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
