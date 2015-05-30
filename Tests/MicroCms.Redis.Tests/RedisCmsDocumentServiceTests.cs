using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Configuration;
using MicroCms.Redis.Configuration;
using MicroCms.Storage;
using MicroCms.Tests;
using Xunit;

namespace MicroCms.Redis.Tests
{
	public class RedisCmsDocumentServiceTests : CmsDocumentServiceTests<RedisCmsDocumentService>
	{
		protected override void ConfigureDocumentService(ICmsConfigurator configurator)
		{
			configurator.UseRedisStorage("");
		}
	}

}
