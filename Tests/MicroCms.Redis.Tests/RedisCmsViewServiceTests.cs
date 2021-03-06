﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Configuration;
using MicroCms.Redis.Configuration;
using MicroCms.Storage;
using MicroCms.Tests;

namespace MicroCms.Redis.Tests
{
	public class RedisCmsViewServiceTests : CmsViewServiceTests<RedisCmsViewService>
	{
		protected override void ConfigureViewService(ICmsConfigurator configurator)
		{
			configurator.UseRedisStorage();
		}
	}
}
