using interview.test.ensek.Core.Contracts;

namespace interview.test.ensek.Client.Uploader.Services
{
    public interface IFileUploaderService
    {
        Task<ImportResultDto> UploadFileAsync(IFormFile file, CancellationToken cancellationToken);
    }
}
