namespace BonsaiShop_API.Areas.Service
{
    public static class ImageHelper
    {
        public static string ConvertImageToBase64(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length <= 0)
                return null;

            using (var ms = new MemoryStream())
            {
                imageFile.CopyTo(ms);
                var imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}
