using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DocStore.Contract.Entities;
using DocStore.Contract.Repositories;
using DocStore.Domain.Helper;
using Microsoft.Extensions.Logging;

namespace DocStore.Domain.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DatabaseHelper helper;
        private readonly ILogger<DocumentRepository> logger;

        public DocumentRepository(DatabaseHelper _helper, ILogger<DocumentRepository> _logger)
        {
            helper = _helper;
            logger = _logger;
        }

        public Document FindById(string documentId)
        {
            try
            {
                var sqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("@documentId", documentId),
                };

                var table = helper.ExecuteSelectQuery("uspGetDocument", sqlParameters);
                if (table.Rows.Count == 0)
                {
                    return null;
                }

                var dataRow = table.Rows[0];
                return new Document
                {
                    DocumentId = dataRow["userId"].ToString(),
                    DocumentName = dataRow["userEmailId"].ToString(),
                    DocumentPath = Convert.ToString(dataRow["userEmailIdVerified"]),
                    DocumentVersion = Convert.ToInt16(dataRow["userGender"]),
                    DocumentOwnerId = Convert.ToString(dataRow["userIsActive"]),
                    CreatedOn = Convert.ToDateTime(dataRow["createdOn"]),
                    ModifiedOn = Convert.ToDateTime(dataRow["modifiedOn"])
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public Document Add(Document document)
        {
            try
            {
                var documentId = Guid.NewGuid().ToString();
                var sqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("@documentId", documentId),
                    new SqlParameter("@documentName", document.DocumentName),
                    new SqlParameter("@documentPath", document.DocumentPath),
                    new SqlParameter("@documentVersion", document.DocumentVersion),
                    new SqlParameter("@documentOwnerId", document.DocumentOwnerId)
                };

                helper.ExecuteQuery("uspInsertDocument", sqlParameters);
                return FindById(documentId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
