using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MicroCms.Sql.DataAccess
{
	class SqlCmsDbContext : DbContext 
	{
		public DbSet<Entity> Entities { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<EntityTag> EntityTags { get; set; }

		public SqlCmsDbContext()
		{
			// Hack to prevent runtime error:
			// System.InvalidOperationException: No Entity Framework provider found for the ADO.NET provider
			// with invariant name 'System.Data.SqlClient'...
			// For more info, see http://robsneuron.blogspot.com/2013/11/entity-framework-upgrade-to-6.html
			var ensureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
		}
	}
}
