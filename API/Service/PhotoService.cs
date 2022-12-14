using API.Helpers;
using API.Interface;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary cloudinary;
    public PhotoService(IOptions<CloudinarySettings> config)
    {
        cloudinary = new(new Account(
            config.Value.CloudName,
            config.Value.Apikey,
            config.Value.ApiSecret
        ));
    }

    public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
    {
        if (file is { Length: <= 0 }) return new();

        return await cloudinary.UploadAsync(new ImageUploadParams() {
            File = new(file.FileName, file.OpenReadStream()),
            Transformation = new Transformation().Height(599).Width(500).Crop("fill").Gravity("face"),
            Folder = "da-net7"
        });
    }

    public async Task<DeletionResult> DeletePhotoAsync(string publicId) => 
        await cloudinary.DestroyAsync(new(publicId));
}
