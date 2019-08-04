using System.Net;
using DocStore.Api.ViewModels;
using DocStore.Contract.Manager;
using DocStore.Contract.Models;
using DocStore.Domain.Helper;
using DocStore.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DocStore.Api.Controllers
{
    [Route("Documents")]
    [ApiController]
    public class DocumentsController : BaseController
    {
        private readonly IHostingEnvironment environment;
        private readonly IDocumentManager documentManager;
        private readonly FileUploadHelper fileUploadHelper;

        public DocumentsController(IHostingEnvironment _environment, IDocumentManager _documentManager, FileUploadHelper _fileUploadHelper)
        {
            environment = _environment;
            documentManager = _documentManager;
            fileUploadHelper = _fileUploadHelper;
        }


        /// <summary>
        /// This endpoint used to upload document.
        /// </summary>
        /// <param name="addDocumentRequestVm"></param>
        /// <returns>Returns uploaded document detail.</returns>
        /// <response code="200">If upload successfully, make entry in database return document detail.</response>
        /// <response code="409">If document already exist, return conflict.</response> 
        /// <response code="500">If something goes wrong while uploading document return null.</response>
        [HttpPost]
        public IActionResult AddDocument(AddDocumentRequestVm addDocumentRequestVm)
        {
            var uploadResult = fileUploadHelper.UploadFile(environment.WebRootPath, CurrentUser.UserId, addDocumentRequestVm.Directory,
                                                           addDocumentRequestVm.Version, addDocumentRequestVm.File);

            if (uploadResult.Item1 == HttpStatusCode.Conflict)
            {
                return Conflict();
            }

            if (uploadResult.Item1 == HttpStatusCode.InternalServerError)
            {
                return StatusCode(500);
            }

            var filePath = uploadResult.Item2;
            var documentDm = new DocumentDm
            {
                DocumentName = addDocumentRequestVm.File.FileName,
                DocumentOwnerId = CurrentUser.UserId,
                DocumentVersion = addDocumentRequestVm.Version,
                DocumentPath = filePath
            };

            var addedDocument = documentManager.Add(documentDm);
            return Ok(addedDocument);
        }
    }
}