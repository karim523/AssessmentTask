using Microsoft.AspNetCore.Http;

namespace Demo.BLL.Common.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        public readonly List<string> _allowedExtensions= new() { ".png",".jgp", ".jpeg" };
        public const int _maxSize = 2_097_152;

        public async Task<string?> UploadAsync(IFormFile file, string folderName)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!_allowedExtensions.Contains(extension))
                return null;

            if (file.Length > _maxSize)
                return null;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            var fileName =$"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(folderName, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            
            await file.CopyToAsync(fileStream);
            
            return fileName;
        }
        public bool Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}
