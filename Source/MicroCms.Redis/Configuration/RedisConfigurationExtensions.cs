using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Configuration;

namespace MicroCms.Redis.Configuration
{
	public static class RedisConfigurationExtensions
	{
        public static ICmsConfigurator UseRedisStorage(this ICmsConfigurator configurator, string configuration = "")
        {
			configurator.UseDocService<RedisCmsDocumentService>(configuration);
			configurator.UseViewService<RedisCmsViewService>(configuration);
            return configurator;
        }
	}
}
