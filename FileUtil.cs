namespace SteelDoorRecipeAPIOdata.Controllers
{
    // the system.drawing is not cross platform and does not work with .netcore
    // we could install a library to check image types
    // but instead I am going to read the image headers to determine what is what
    public static class FileUtil
    {
        private static string baseFilePath = "C:\\";//System.Configuration.ConfigurationManager.AppSettings["Root"].ToString();
        private const string SUCCESS = "Create Success!";
        private static byte[] SIGNATURE_PNG = new byte[8] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        private static byte[] SIGNATURE_JPEG = new byte[4] { 0xFF, 0xD8, 0xFF, 0xD9 };
        public static async Task<byte[]> GetFile(string filePath)
        {
            byte[] image = await File.ReadAllBytesAsync(filePath);

            return image;
        }
        public static async Task SaveFile(byte[] image, string root, int id)
        {
            string extension = DetermineFileType(image);

            if (!extension.Equals(""))
            {
                try
                {
                    string filePath = baseFilePath + "\\" + root + "\\" + id + extension;
                    await File.WriteAllBytesAsync(filePath, image);
                    Console.WriteLine(SUCCESS);
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); 
                }
            }
        }
        public static void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }
        public static string DetermineFileType(byte[] image)
        {
            string result = "";

            if (!image.Equals(null))
            {
                // check if png
                // png has 8 header bytes which we can use to determine if
                // the image is of type png
                if (SIGNATURE_PNG[0].Equals(image[0]))
                {
                    result = ".png";
                    // we've checked the first byte, ignore it now
                    for (int i = 1; i < SIGNATURE_PNG.Length; i++)
                    {
                        if (!SIGNATURE_PNG[i].Equals(image[i]))
                        {
                            result = "";
                            break;
                        }
                    }
                } 
                // check if jpeg
                // jpeg starts with two header bytes and has two ender bytes
                // however is less standardized than png
                else if(SIGNATURE_JPEG[0].Equals(image[0]))
                {
                    // [^n] is shorthand for array.Length -n
                    if (SIGNATURE_JPEG[1].Equals(image[1]) &&
                        SIGNATURE_JPEG[2].Equals(image[^2]) &&
                        SIGNATURE_JPEG[3].Equals(image[^1]))
                        result = ".jpg";
                }
            }

            return result;
        }
    }
}
