namespace Edukate.Helpers
{
    public static class ImageHelper
    {
        public static bool IsValidSize(this IFormFile imageFile, float kb = 200)
            => imageFile.Length <= kb * 1024;

        public static bool IsValidType(this IFormFile imageFile)
            => imageFile.ContentType.Contains("image");

        public static async Task<string> SaveImageAsync(this IFormFile imageFile, string saveImageToPath)
        {
            string imageFileName = Guid.NewGuid() + imageFile.FileName;
            string imageFilePath = Path.Combine(PathConstants.RootPath, saveImageToPath, imageFileName);

            var filePath = Path.Combine(PathConstants.RootPath, saveImageToPath);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            using (FileStream fs = new(imageFilePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fs);
            }

            return imageFileName;
        }
    }
}
