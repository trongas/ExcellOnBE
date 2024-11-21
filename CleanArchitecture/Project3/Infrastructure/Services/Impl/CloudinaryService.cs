using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Impl
{
    public class CloudinaryService : ICloudinaryService
    {
        private Account account { get; set; }
        private Cloudinary cloudinary { get; set; }
        public CloudinaryService()
        {
            account = new Account("dizrgt9rd", "181289387798293", "k39x00mNyaOpL-zjsAXRZ6crOGI");
            cloudinary = new Cloudinary(account);
        }

        public ImageUploadResult Upload(IFormFile file, string PublicId)
        {
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = PublicId
                };

                var uploadResult = cloudinary.Upload(uploadParams);

                return uploadResult;
            }
        }
    }
}
