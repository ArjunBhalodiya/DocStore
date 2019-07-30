using System;
using System.IO;
using System.Net.Http.Headers;
using DocStore.Contract.Manager;
using DocStore.Contract.Models;
using DocStore.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocStore.Api.Controllers
{
    [Route("Documents")]
    [ApiController]
    public class DocumentsController : BaseController
    {
        private readonly IDocumentManager documentManager;
        private readonly ILogger<UsersController> logger;
        private readonly IHostingEnvironment hostingEnvironment;

        public DocumentsController(IDocumentManager documentManager, ILogger<UsersController> logger, IHostingEnvironment hostingEnvironment)
        {
            this.documentManager = documentManager;
            this.logger = logger;
            this.hostingEnvironment = hostingEnvironment;
        }


        /// <summary>
        /// This endpoint used to upload document.
        /// </summary>
        /// <param name="directory"></param>
        /// <returns>Returns uploaded document detail.</returns>
        /// <response code="200">If upload successfully, make entry in database return document detail.</response>
        /// <response code="409">If document already exist, return conflict.</response> 
        /// <response code="500">If something goes wrong while uploading document return null.</response>
        [HttpPost]
        public IActionResult AddDocument([FromQuery]string directory)
        {
            try
            {
                var file = Request.Form.Files[0];

                if (file == null || file.Length == 0)
                    return BadRequest();

                var newPath = Path.Combine(hostingEnvironment.WebRootPath,
                                           string.IsNullOrEmpty(directory) ? $"documents/{CurrentUser.UserId}"
                                                                           : $"documents/{directory}/{CurrentUser.UserId}");
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(newPath, fileName);

                if (System.IO.File.Exists(fullPath))
                {
                    return Conflict();
                }

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var documentDm = new DocumentDm
                {
                    DocumentName = fileName,
                    DocumentOwnerId = CurrentUser.UserId,
                    DocumentPath = fullPath
                };

                var addedDocument = documentManager.AddDocument(documentDm);
                return Ok(addedDocument);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}