using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroCms.Search;

namespace MicroCms.Storage
{
    public class MemoryCmsDocumentService : MemoryCmsEntityService<CmsDocument>, ICmsDocumentService
    {
        public MemoryCmsDocumentService()
            : this(new ConcurrentDictionary<Guid, CmsDocument>())
        {
        }

        public MemoryCmsDocumentService(ConcurrentDictionary<Guid, CmsDocument> entities)
            : base(entities)
        {
        }

        public ICmsSearchService Search { get; set; }

        public override IEnumerable<CmsTitle> GetAll()
        {
            if (Search != null)
                return Search.GetAll();

            return base.GetAll();
        }

        public override IEnumerable<CmsTitle> GetByTag(string tag)
        {
            if (Search != null)
                return Search.SearchDocuments(CmsDocumentField.Tag, tag);

            return GetAll().Where(e => e.Tags.Any(t => tag.Equals(t, StringComparison.InvariantCultureIgnoreCase)));
        }

        public override void Save(CmsDocument entity)
        {
            base.Save(entity);
            if (Search != null)
                Search.AddOrUpdateDocuments(entity);
        }

        public override CmsDocument Delete(Guid id)
        {
            var document = base.Delete(id);
            if (Search != null)
                Search.DeleteDocuments(document);
            return document;
        }
    }
}