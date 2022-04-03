using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata
{
    public partial class Instruction
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }
        public string StepWithDoneness { get; set; } = null!;

        public virtual Recipe Recipe { get; set; } = null!;
    }
}
