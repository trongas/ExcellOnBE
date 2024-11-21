using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public interface ICloudinaryService
    {
        public ImageUploadResult Upload(IFormFile file , string PublicId);
    }
}
