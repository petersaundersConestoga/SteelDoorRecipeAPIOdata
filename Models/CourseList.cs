using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata
{
    public partial class CourseList
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int RecipeId { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
