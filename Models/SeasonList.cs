using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata.Models
{
    public partial class SeasonList
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int SeasonId { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;
        public virtual Season Season { get; set; } = null!;
    }
}
