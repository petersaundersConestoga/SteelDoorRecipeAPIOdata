using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata.Models
{
    public partial class ImageRecipe
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Location { get; set; } = null!;

        public virtual Recipe Recipe { get; set; } = null!;
    }
}
