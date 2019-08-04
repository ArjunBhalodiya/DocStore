using DocStore.Contract.Entities;

namespace DocStore.Contract.Repositories
{
    public interface IDocumentRepository
    {
        Document FindById(string documentId);
        Document Add(Document document);
    }
}