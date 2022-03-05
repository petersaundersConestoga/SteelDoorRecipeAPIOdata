using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata.Models
{
    public partial class Timing
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public TimeSpan? Preparation { get; set; }
        public TimeSpan? Cooking { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;
    }
}
