using Microsoft.AspNetCore.Http;

namespace DocStore.Api.ViewModels
{
    public class AddDocumentRequestVm
    {
        public string Directory { get; set; }
        public int Version { get; set; }
        public IFormFile File { get; set; }
    }
}