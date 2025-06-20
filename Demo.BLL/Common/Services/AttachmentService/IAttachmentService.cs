using Microsoft.AspNetCore.Http;

namespace Demo.BLL.Common.Services.AttachmentService
{
    public interface IAttachmentService
    {
        public Task<string?> UploadAsync(IFormFile file,string folderName);
        public bool Delete(string filePath);
    }
}
