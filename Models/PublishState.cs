using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata
{
    public partial class PublishState
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int ReviewId { get; set; }
        public int State { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;
        public virtual Review Review { get; set; } = null!;
    }
}
