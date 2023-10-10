using Microsoft.AspNetCore.Http;

namespace PersonalLibrary.Utilities.Managers;
public static class ImageManager
{
    public static string? SaveImageToLocalStorage(IFormFile? imageFile, string path)
    {
        if (imageFile == null) return null;
        
        byte[] byteArray;
    
        using (var binaryReader = new BinaryReader(imageFile.OpenReadStream()))
        {
            byteArray = binaryReader.ReadBytes((int)imageFile.Length);
        }
        
        // Converting image to base64 string.
        var base64Image = Convert.ToBase64String(byteArray);

        var imageExtension = "jpeg";
        var image = base64Image;
        
        // Creating image name.
        var imageName = Guid.NewGuid().ToString() + "." + imageExtension;
        
        // Saving image to local storage.
        var imagePath = Path.Combine(path, imageName);
        var bytes = Convert.FromBase64String(image);
        File.WriteAllBytes(imagePath, bytes);
        
        return imageName;
    }
    
    public static byte[]? ReadImageFromLocalStorage(string? imageName, string path)
    {
        if (imageName == null) return null;

        var imagePath = Path.Combine(path, imageName);
        return File.ReadAllBytes(imagePath);
    }
}