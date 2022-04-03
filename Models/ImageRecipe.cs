using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata.Models
{
    public partial class ImageRecipe
    {
        public ImageRecipe()
        {
            
        }
        public ImageRecipe(ImageRecipeImplementation image)
        {
            this.Id = image.Id;
            this.Location = image.Location;
            this.RecipeId = image.RecipeId;
        }
        public int Id { get; set; }
        public int RecipeId { get; set; }
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
