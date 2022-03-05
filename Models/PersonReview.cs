﻿using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata.Models
{
    public partial class PersonReview
    {
        public int RecipeId { get; set; }
        public int PersonId { get; set; }
        public int ReviewId { get; set; }

        public virtual Person Person { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
        public virtual Review Review { get; set; } = null!;
    }
}
