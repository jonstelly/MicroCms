using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Configuration;
using MicroCms.Sql.Configuration;
using MicroCms.Storage;
using MicroCms.Tests;

namespace MicroCms.Sql.Tests
{
	public class SqlCmsViewServiceTests : CmsViewServiceTests<SqlCmsViewService>
	{
		protected override void ConfigureViewService(ICmsConfigurator configurator)
		{
			configurator.UseSqlStorage();
		}
	}
}
