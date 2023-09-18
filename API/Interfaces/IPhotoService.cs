
using CloudinaryDotNet.Actions;

namespace API.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file); // Add the IFormFile interface to the IPhotoService interface.
        Task<DeletionResult> DeletePhotoAsync(string publicId); // Add the DeletePhotoAsync method to the IPhotoService interface.
        
    }
}