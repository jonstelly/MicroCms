﻿using System;
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

        public override CmsDocument Delete(Guid id)
        {
            var document = base.Delete(id);
            var search = Cms.CreateContext().Search;
            if (search != null)
                search.DeleteDocuments(document);
            return document;
        }

        public override void Save(CmsDocument entity)
        {
            base.Save(entity);
            var search = Cms.CreateContext().Search;
            if (search != null)
                search.AddOrUpdateDocuments(entity);
        }

        public override IEnumerable<CmsTitle> GetAll()
        {
            var search = Cms.CreateContext().Search;
            if (search != null)
                return search.GetAll();

            return base.GetAll();
        }

        public override IEnumerable<CmsTitle> GetByTag(string tag)
        {
            var search = Cms.CreateContext().Search;
            if (search != null)
                return search.SearchDocuments(CmsDocumentField.Tag, tag);

            return GetAll().Where(e => e.Tags.Any(t => tag.Equals(t, StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}