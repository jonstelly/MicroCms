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
		/// <summary>
		/// Configuration for using Redis as a storage option
		/// </summary>
		/// <param name="configurator"></param>
		/// <param name="connectionString">should be in the format server:port (defaults to 127.0.0.1:6379)</param>
		/// <returns></returns>
        public static ICmsConfigurator UseRedisStorage(this ICmsConfigurator configurator, string connectionString = "")
        {
			configurator.UseDocService<RedisCmsDocumentService>(connectionString);
			configurator.UseViewService<RedisCmsViewService>(connectionString);
            return configurator;
        }
	}
}
