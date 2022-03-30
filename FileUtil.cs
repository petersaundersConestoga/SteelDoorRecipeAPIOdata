namespace SteelDoorRecipeAPIOdata.Controllers
{
    public class FileUtil
    {
        public async Task<byte[]> GetFile(string filePath)
        {
            byte[] image = File.ReadAllBytes(filePath);

            return image;
        }
    }
}
