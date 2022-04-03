using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata
{
    public partial class Diet
    {
        public Diet()
        {
            DietLists = new HashSet<DietList>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<DietList> DietLists { get; set; }
    }
}
