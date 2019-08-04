using DocStore.Contract.Entities;
using DocStore.Contract.Manager;
using DocStore.Contract.Models;
using DocStore.Contract.Repositories;

namespace DocStore.Domain.Manager
{
    public class DocumentManager : IDocumentManager
    {
        private readonly IDocumentRepository documentRepository;

        public DocumentManager(IDocumentRepository _documentRepository)
        {
            documentRepository = _documentRepository;
        }

        public DocumentDm FindById(string documentId)
        {
            var document = documentRepository.FindById(documentId);
            if (document == null)
            {
                return null;
            }

            return new DocumentDm
            {
                DocumentId = document.DocumentId,
                DocumentName = document.DocumentName,
                DocumentPath = document.DocumentPath,
                DocumentVersion = document.DocumentVersion,
                DocumentOwnerId = document.DocumentOwnerId,
                CreatedOn = document.CreatedOn,
                ModifiedOn = document.ModifiedOn
            };
        }

        public DocumentDm Add(DocumentDm documentDm)
        {
            var document = documentRepository.Add(new Document
            {
                DocumentId = documentDm.DocumentId,
                DocumentName = documentDm.DocumentName,
                DocumentPath = documentDm.DocumentPath,
                DocumentVersion = documentDm.DocumentVersion,
                DocumentOwnerId = documentDm.DocumentOwnerId,
                CreatedOn = documentDm.CreatedOn,
                ModifiedOn = documentDm.ModifiedOn
            });

            if (document == null)
            {
                return null;
            }

            return new DocumentDm
            {
                DocumentId = document.DocumentId,
                DocumentName = document.DocumentName,
                DocumentPath = document.DocumentPath,
                DocumentVersion = document.DocumentVersion,
                DocumentOwnerId = document.DocumentOwnerId,
                CreatedOn = document.CreatedOn,
                ModifiedOn = document.ModifiedOn
            };
        }
    }
}