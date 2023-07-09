using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using interview.test.ensek.Core.Contracts;

namespace interview.test.ensek.Client.Uploader.Services
{
    public class FileUploaderService : IFileUploaderService
    {
        public FileUploaderService(HttpClient client)
        {
            _client = client;
        }

        private HttpClient _client { get; init; }

        public async Task<ImportResultDto> UploadFileAsync(IFormFile file, CancellationToken cancellationToken)
        {

            using var form = new MultipartFormDataContent();
            using var ms = new MemoryStream();
            
            await file.CopyToAsync(ms, cancellationToken);
            ms.Position = 0;
            using var content = new ByteArrayContent(ms.ToArray());
            content.Headers.ContentType= MediaTypeHeaderValue.Parse("multipart/form-data");

            form.Add(content, "source", file.Name);
            var response = await _client.PostAsync("api/meter-reading-uploads", form);

            if (response.IsSuccessStatusCode == false)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = JsonSerializer.Deserialize<ImportResultDto>(
                dataAsString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return result;
        }
    }
}
