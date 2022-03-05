using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata.Models
{
    public partial class IngredientList
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int UnitId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Quantity { get; set; }
    }
}
