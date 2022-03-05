using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata.Models
{
    public partial class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool? Liquid { get; set; }
        public bool? Weight { get; set; }
        public bool? PhysicalMeasure { get; set; }
    }
}
