using CloudinaryDotNet.Actions;

namespace api;

public interface IImageService
{
  Task<ImageUploadResult> AddImageAsync(IFormFile file);
  Task<DeletionResult> DeleteImageAsync(string publicId); //from Photo.cs -> PublicId
}