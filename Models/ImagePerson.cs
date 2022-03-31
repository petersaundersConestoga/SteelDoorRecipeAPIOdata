using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata.Models
{
    public partial class ImagePerson
    {
        public ImagePerson() { }
        public ImagePerson(ImagePersonImplementation image)
        {
            this.Id = image.Id;
            this.PersonId = image.PersonId;
            this.Location = "";
        }
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Location { get; set; } = null!;

        public virtual Person Person { get; set; } = null!;
    }
}
