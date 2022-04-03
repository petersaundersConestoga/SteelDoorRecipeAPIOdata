using SteelDoorRecipeAPIOdata.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SteelDoorRecipeAPIOdata
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
        [JsonIgnore]
        public string Location { get; set; } = null!;

        public virtual Person Person { get; set; } = null!;

        internal void Patch(ImagePerson image)
        {
            this.Id = image.Id;
            this.Person = image.Person;
            this.Location = image.Location;
        }
    }
}
