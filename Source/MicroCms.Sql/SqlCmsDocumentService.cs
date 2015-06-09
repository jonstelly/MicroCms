using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Search;
using MicroCms.Sql.DataAccess;
using MicroCms.Storage;

namespace MicroCms.Sql
{
    public class SqlCmsDocumentService : SqlCmsEntityService<CmsDocument>, ICmsDocumentService
    {
        public const string ENTITY_TYPE = "document";
        public override string EntityType { get { return ENTITY_TYPE; } }

	    public SqlCmsDocumentService(ISqlCmsDbContext sqlCmsDbContext) : base(sqlCmsDbContext){}

        public ICmsSearchService Search { get; set; }

        public override CmsDocument Delete(Guid id)
        {
            CmsDocument cmsDocument = base.Delete(id);
            if (Search != null)
                Search.DeleteDocuments(cmsDocument);
            return cmsDocument;
        }

        public override void Save(CmsDocument entity)
        {
            base.Save(entity);

            if (Search != null)
                Search.AddOrUpdateDocuments(entity);
        }
    }
}
