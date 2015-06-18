using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Search;
using MicroCms.Sql.DataAccess;
using MicroCms.Storage;

namespace MicroCms.Sql
{
    public class SqlCmsViewService : SqlCmsEntityService<CmsView>, ICmsViewService
    {
        public const string ENTITY_TYPE = "view";
        public override string EntityType { get { return ENTITY_TYPE; } }

	    public SqlCmsViewService(ISqlCmsDbContext sqlCmsDbContext) : base(sqlCmsDbContext){}
    }
}
