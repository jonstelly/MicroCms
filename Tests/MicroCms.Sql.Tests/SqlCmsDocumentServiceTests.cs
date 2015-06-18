using System;
using MicroCms.Configuration;
using MicroCms.Sql.Configuration;
using MicroCms.Storage;
using MicroCms.Tests;
using Xunit;

namespace MicroCms.Sql.Tests
{
	public class SqlCmsDocumentServiceTests : CmsDocumentServiceTests<SqlCmsDocumentService>
	{
		protected override void ConfigureDocumentService(ICmsConfigurator configurator)
		{
			configurator.UseSqlStorage();
		}
	}
}
