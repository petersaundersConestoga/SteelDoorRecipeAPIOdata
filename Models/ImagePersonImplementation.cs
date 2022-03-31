using Microsoft.AspNetCore.OData.Results;
using SteelDoorRecipeAPIOdata.Controllers;
using System.Collections;
using System.Linq.Expressions;

namespace SteelDoorRecipeAPIOdata.Models
{
    // we cannot work directly with the ImagePerson Model as it depends on the db
    // so we create a new version that can deal with our need to store the byte[]
    public class ImagePersonImplementation : ImagePerson
    {
        public ImagePersonImplementation() { }

        public ImagePersonImplementation(ImagePerson person)
        {
            this.Id = person.Id;
            this.PersonId = person.Id;
            this.Location = "";
            this.Image = FileUtil.GetFile(person.Location).GetAwaiter().GetResult();
        }

        public byte[] Image { get; set; } = new byte[] { 0x00 };
    }
}
