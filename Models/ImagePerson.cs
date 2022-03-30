using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata.Models
{
    public partial class ImagePerson
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Location { get; set; } = null!;

        public virtual Person Person { get; set; } = null!;
    }
}
