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
		private const string ConnactionStringDefault = "127.0.0.1:6379"; // default to localhost and standard Redis port

		private readonly ConnectionMultiplexer _redisConnection;

		public RedisCmsService(string connectionString = "")
		{
			connectionString = (string.IsNullOrWhiteSpace(connectionString)) ? ConnactionStringDefault : connectionString;
			_redisConnection = ConnectionMultiplexer.Connect(connectionString);
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
