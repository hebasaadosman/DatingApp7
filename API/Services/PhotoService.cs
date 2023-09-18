



using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary; // Add a private readonly field of type Cloudinary to the PhotoService class.
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account( // Create a new instance of the Account class and pass in the Cloudinary settings from the appsettings.json file.
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(acc); // Create a new instance of the Cloudinary class and pass in the account details.

        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult(); // Create a new instance of the ImageUploadResult class.
            if (file.Length > 0) // Check if the file has any content.
            {
                using var stream = file.OpenReadStream(); // Create a new instance of the StreamReader class and pass in the file.
                var uploadParams = new ImageUploadParams // Create a new instance of the ImageUploadParams class.
                {
                    File = new FileDescription(file.FileName, stream), // Set the File property of the ImageUploadParams class to a new instance of the FileDescription class and pass in the file name and the stream.
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),// Set the Transformation property of the ImageUploadParams class to a new instance of the Transformation class and set the Height, Width, Crop and Gravity properties.
                    Folder = "dating_app_V7" // Set the Folder property of the ImageUploadParams class to "dating_app".
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams); // Set the uploadResult variable to the result of the UploadAsync method and pass in the uploadParams.
            }
            return uploadResult; // Return the uploadResult variable.

        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId); // Create a new instance of the DeletionParams class and pass in the publicId.
            return await _cloudinary.DestroyAsync(deleteParams); // Set the result variable to the result of the DestroyAsync method and pass in the deleteParams.
            ; // Return the result variable.
        }
    }
}