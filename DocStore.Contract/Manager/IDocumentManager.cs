using System;
using DocStore.Contract.Models;

namespace DocStore.Contract.Manager
{
    public interface IDocumentManager
    {
        DocumentDm AddDocument(DocumentDm documentDm);
    }
}
