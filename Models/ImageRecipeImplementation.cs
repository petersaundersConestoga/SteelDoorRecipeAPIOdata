using Microsoft.AspNetCore.OData.Results;
using SteelDoorRecipeAPIOdata.Controllers;
using System.Collections;
using System.Linq.Expressions;

namespace SteelDoorRecipeAPIOdata.Models
{
    // we cannot work directly with the ImagePerson Model as it depends on the db
    // so we create a new version that can deal with our need to store the byte[]
    // https://weblogs.asp.net/mehfuzh/writing-custom-linq-provider#comment-627
    public class ImageRecipeImplementation : ImageRecipe 
    {
        public ImageRecipeImplementation() { }

        public ImageRecipeImplementation(ImageRecipe recipe)
        {
            this.Id = recipe.Id;
            this.RecipeId = recipe.RecipeId;
            this.Location = "";
            this.Image = FileUtil.GetFile(recipe.Location).GetAwaiter().GetResult();
            this.FileType = recipe.Location.Substring(recipe.Location.Length - 3);
        }
        public byte[] Image { get; set; } = new byte[] { 0x00 };
        public string FileType { get; set; } = "";
    }
}
