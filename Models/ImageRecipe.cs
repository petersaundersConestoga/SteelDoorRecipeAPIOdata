using SteelDoorRecipeAPIOdata.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SteelDoorRecipeAPIOdata
{
    public partial class ImageRecipe
    {
        public ImageRecipe() { }
        public ImageRecipe(ImageRecipeImplementation image)
        {
            this.Id = image.Id;
            this.Location = image.Location;
            this.RecipeId = image.RecipeId;
        }
        public int Id { get; set; }
        public int RecipeId { get; set; }
        [JsonIgnore]
        public string Location { get; set; } = null!;

        public virtual Recipe Recipe { get; set; } = null!;

        public void Patch(ImageRecipe image)
        {
            this.Id = image.Id;
            this.Location = image.Location;
            this.RecipeId = image.RecipeId;
        }
    }
}
