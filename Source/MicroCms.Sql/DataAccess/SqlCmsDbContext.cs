using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MicroCms.Sql.Configuration;

namespace MicroCms.Sql.DataAccess
{
	[DbConfigurationType(typeof(SqlCmsDbConfiguration))] 
	public class SqlCmsDbContext : DbContext, ISqlCmsDbContext
	{
		public virtual DbSet<Entity> Entities { get; set; }
		public virtual DbSet<Tag> Tags { get; set; }
		public virtual DbSet<EntityTag> EntityTags { get; set; }

		public SqlCmsDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
		{
			// Hack to prevent runtime error:
			// System.InvalidOperationException: No Entity Framework provider found for the ADO.NET provider
			// with invariant name 'System.Data.SqlClient'...
			// For more info, see http://robsneuron.blogspot.com/2013/11/entity-framework-upgrade-to-6.html
			var ensureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
		}
	}
}
