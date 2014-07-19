using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MicroCms.Search;

namespace MicroCms.Storage
{
    public class MemoryCmsDocumentService : MemoryCmsEntityService<CmsDocument>, ICmsDocumentService
    {
        public virtual IEnumerable<CmsDocument> GetByPath(string path)
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
                foreach (var document in GetAll().Where(d => d.Path == path.ToLowerInvariant()))
                {
                    yield return document;
                }
            }
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
    }
}