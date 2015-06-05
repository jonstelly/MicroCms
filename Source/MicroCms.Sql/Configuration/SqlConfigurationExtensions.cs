using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Configuration;
using MicroCms.Storage;

namespace MicroCms.Sql.Configuration
{
	public static class SqlConfigurationExtensions
	{
		public static ICmsConfigurator UseSqlStorage(this ICmsConfigurator configurator)
		{
			configurator.UseDocService<SqlCmsDocumentService>();
			configurator.UseViewService<SqlCmsViewService>();
			return configurator;
		}

		public static ICmsConfigurator UseSqlSearch(this ICmsConfigurator configurator)
		{
			return configurator.UseSearch<SqlCmsSearchService>();
		}

	}
}
