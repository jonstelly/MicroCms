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
        public FSCmsDocumentService(DirectoryInfo directory)
            : base(directory, "Documents")
        {
        }

        public ICmsSearchService Search { get; set; }

        public override CmsDocument Delete(Guid id)
        {
            var document = base.Delete(id);
            if (Search != null)
                Search.DeleteDocuments(document);
            return document;
        }

        public override void Save(CmsDocument entity)
        {
            base.Save(entity);
            if (Search != null)
                Search.AddOrUpdateDocuments(entity);
        }

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
    }
}