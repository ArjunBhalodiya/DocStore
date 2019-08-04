using System;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DocStore.Domain.Helper
{
    public class FileUploadHelper
    {
        private readonly ILogger<FileUploadHelper> logger;

        public FileUploadHelper(ILogger<FileUploadHelper> _logger)
        {
            logger = _logger;
        }

        public Tuple<HttpStatusCode, string> UploadFile(string rootPath, string userId, string directory, int version, IFormFile file)
        {
            try
            {
                var newPath = Path.Combine(rootPath, string.IsNullOrEmpty(directory) ? $"documents/{userId}/{version}"
                                                                                    : $"documents/{userId}/{directory}/{version}");

                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(newPath, fileName);

                if (File.Exists(fullPath))
                {
                    return new Tuple<HttpStatusCode, string>(HttpStatusCode.Conflict, "File already exist");
                }

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return new Tuple<HttpStatusCode, string>(HttpStatusCode.Created, fullPath);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new Tuple<HttpStatusCode, string>(HttpStatusCode.InternalServerError, "Something went wrong while uploading file.");
            }
        }
    }
}
