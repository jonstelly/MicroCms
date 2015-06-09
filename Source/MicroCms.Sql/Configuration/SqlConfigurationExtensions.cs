using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Configuration;
using MicroCms.Sql.DataAccess;
using MicroCms.Storage;

namespace MicroCms.Sql.Configuration
{
	public static class SqlConfigurationExtensions
	{
		private static ISqlCmsDbContext _sqlCmsDbContext;

		public static ICmsConfigurator UseSqlStorage(this ICmsConfigurator configurator, string nameOrConnectionString = null)
		{
			//configurator.RegisterType(typeof(ISqlCmsDbContext), typeof(SqlCmsDbContext), null, nameOrConnectionString);
			configurator.UseDocService<SqlCmsDocumentService>(GetSqlCmsDbContext(nameOrConnectionString));
			configurator.UseViewService<SqlCmsViewService>(GetSqlCmsDbContext(nameOrConnectionString));
			return configurator;
		}

		public static ICmsConfigurator UseSqlSearch(this ICmsConfigurator configurator, string nameOrConnectionString = null)
		{
			return configurator.UseSearch<SqlCmsSearchService>(GetSqlCmsDbContext(nameOrConnectionString));
		}

		private static ISqlCmsDbContext GetSqlCmsDbContext(string nameOrConnectionString = null)
		{
			return _sqlCmsDbContext ?? (_sqlCmsDbContext = new SqlCmsDbContext(nameOrConnectionString));
		}
	}
}
