using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MicroCms.Search;

namespace MicroCms.Storage
{
    public class MemoryContentDocumentRepository : MemoryContentRepository<ContentDocument>, IContentDocumentRepository
    {
        public virtual IEnumerable<ContentDocument> GetByPath(string path)
        {
            var search = Cms.GetArea().ContentSearch;
            if (search != null)
            {
                foreach (var result in search.SearchDocuments(DocumentField.Path, path))
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

        public override void Save(ContentDocument entity)
        {
            base.Save(entity);
            var search = Cms.GetArea().ContentSearch;
            if (search != null)
            {
                search.AddOrUpdateDocuments(entity);
            }
        }
    }
}