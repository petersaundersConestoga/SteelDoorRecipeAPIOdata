using Microsoft.AspNetCore.OData.Results;
using SteelDoorRecipeAPIOdata.Controllers;
using System.Collections;
using System.Linq.Expressions;

namespace SteelDoorRecipeAPIOdata.Models
{
    // we cannot work directly with the ImagePerson Model as it depends on the db
    // so we create a new version that can deal with our need to store the byte[]
    // https://weblogs.asp.net/mehfuzh/writing-custom-linq-provider#comment-627
    public class ImagePersonImplementation : ImagePerson //, IQueryable//, IQueryProvider
    {
        public ImagePersonImplementation() { }

        public ImagePersonImplementation(ImagePerson person)
        {
            this.Id = person.Id;
            this.PersonId = person.Id;
            this.Location = "";
            this.Image = FileUtil.GetFile(person.Location).GetAwaiter().GetResult();
            this.FileType = person.Location.Substring(person.Location.Length - 3);
        }
        public byte[] Image { get; set; } = new byte[] { 0x00 };
        public string FileType { get; set; } = "";
    }
}
