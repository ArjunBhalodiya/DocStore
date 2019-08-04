using System;
using DocStore.Contract.Models;

namespace DocStore.Contract.Manager
{
    public interface IDocumentManager
    {
        DocumentDm FindById(string documentId);
        DocumentDm Add(DocumentDm document);
    }
}
