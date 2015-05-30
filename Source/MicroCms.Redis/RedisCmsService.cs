using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace MicroCms.Redis
{
	public class RedisCmsService
	{
		private const string ConfigurationDefault = "127.0.0.1:6379";

		private readonly ConnectionMultiplexer _redisConnection;

		public RedisCmsService(string configuration = "")
		{
			configuration = (string.IsNullOrWhiteSpace(configuration)) ? ConfigurationDefault : configuration;
			_redisConnection = ConnectionMultiplexer.Connect(configuration);
		}

		public string Get(string key)
		{
			var db = _redisConnection.GetDatabase();
			string value = db.StringGet(key.ToLower());
			return value;
		}

		public void Set(string key, string value)
		{
			var db = _redisConnection.GetDatabase();
			db.StringSet(key.ToLower(), value);
		}

		public void Delete(string key)
		{
			var db = _redisConnection.GetDatabase();
			db.KeyDelete(key.ToLower());
		}

		public List<string> GetAllKeys(string startsWith = "")
		{
			var keysList = new List<string>();
			foreach (var endPoint in _redisConnection.GetEndPoints())
			{
				var server = _redisConnection.GetServer(endPoint);
				foreach (var key in server.Keys(pattern: startsWith + "*"))
					keysList.Add(key);
			}
			return keysList;
		}

	}
}
