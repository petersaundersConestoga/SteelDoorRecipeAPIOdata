﻿using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata.Models
{
    public partial class Cuisine
    {
        public Cuisine()
        {
            Recipes = new HashSet<Recipe>();
        }

        public int Id { get; set; }
        public string? Region { get; set; }
        public string? Country { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
