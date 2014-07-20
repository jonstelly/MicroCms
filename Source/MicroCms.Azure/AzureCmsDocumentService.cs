using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Search;
using MicroCms.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace MicroCms.Azure
{
    public class AzureCmsDocumentService : AzureCmsEntityService<CmsDocument>, ICmsDocumentService
    {
        public AzureCmsDocumentService(CloudBlobClient client, string directory = "documents", string container = "cms")
            : base(client, directory, container)
        {
        }

        public override void Save(CmsDocument entity)
        {
            base.Save(entity);
            var search = Cms.GetArea().Search;
            if (search != null)
            {
                search.AddOrUpdateDocuments(entity);
            }
        }

        public IEnumerable<CmsDocument> GetByPath(string path)
        {
            var search = Cms.GetArea().Search;
            if (search != null)
            {
                foreach (var result in search.SearchDocuments(CmsDocumentField.Path, path))
                {
                    yield return Find(result.Id);
                }
            }
            else
            {
                foreach (var document in GetAll())
                {
                    if (!String.IsNullOrEmpty(document.Path) && document.Path.ToLowerInvariant() == path)
                        yield return document;
                }
            }
        }
    }
}