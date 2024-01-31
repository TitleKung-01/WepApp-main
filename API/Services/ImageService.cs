using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace api;

public class ImageService : IImageService
{
  private readonly Cloudinary _cloudinary;
  public ImageService(IOptions<CloudinarySettings> conf)
  {
    _cloudinary = new Cloudinary(new Account(
        conf.Value.CloudName,
        conf.Value.ApiKey,
        conf.Value.ApiSecret
    ));
  }
  public async Task<ImageUploadResult> AddImageAsync(IFormFile file)
  {
    var uploadResualt = new ImageUploadResult();
    if (file.Length > 0)
    {
      using var stream = file.OpenReadStream();
      var uploadParams = new ImageUploadParams
      {
        File = new FileDescription(file.FileName, stream),
        Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
        Folder = "dotnet7-tinner"
      };
      uploadResualt = await _cloudinary.UploadAsync(uploadParams);
    }
    return uploadResualt;
  }

  public async Task<DeletionResult> DeleteImageAsync(string publicId)
  {
    var deleteParams = new DeletionParams(publicId);
    return await _cloudinary.DestroyAsync(deleteParams);
  }

}