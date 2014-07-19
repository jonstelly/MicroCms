using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MicroCms.Search;

namespace MicroCms.Storage
{
    public class FSCmsDocumentService : FSCmsEntityService<CmsDocument>, ICmsDocumentService
    {
        public FSCmsDocumentService(DirectoryInfo baseDirectory)
            : base(new DirectoryInfo(Path.Combine(baseDirectory.FullName, "Documents")))
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