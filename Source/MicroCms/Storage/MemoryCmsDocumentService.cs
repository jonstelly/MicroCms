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
        public override IEnumerable<CmsTitle> GetAll()
        {
            var search = Cms.GetArea().Search;
            if (search != null)
                return search.GetAll();

            return base.GetAll();
        }

        public override IEnumerable<CmsTitle> GetByTag(string tag)
        {
            var search = Cms.GetArea().Search;
            if (search != null)
                return search.SearchDocuments(CmsDocumentField.Tag, tag);

            return GetAll().Where(e => e.Tags.Any(t => tag.Equals(t, StringComparison.InvariantCultureIgnoreCase)));
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

        public override CmsDocument Delete(Guid id)
        {
            var document = base.Delete(id);
            var search = Cms.GetArea().Search;
            if (search != null)
                search.DeleteDocuments(document);
            return document;
        }
    }
}