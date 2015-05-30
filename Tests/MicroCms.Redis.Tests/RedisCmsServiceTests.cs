using System;
using Xunit;

namespace MicroCms.Redis.Tests
{
	public class RedisCmsServiceTests
	{
		const string TestKey = "TestKey";
		const string TestValue = "Test Value";

		[Fact]
		public void SetSucceeds()
		{
			// This also verifies that Get() is working as well
			var redisCmsService = new RedisCmsService();
			redisCmsService.Set(TestKey, TestValue);
			var retrievedValue = redisCmsService.Get(TestKey);
			Assert.Equal(TestValue, retrievedValue);
		}

		[Fact]
		public void DeleteSucceeds()
		{
			// This also verifies that Set() and Get() are working as well
			var redisCmsService = new RedisCmsService();
			redisCmsService.Set(TestKey, TestValue);
			var retrievedValue = redisCmsService.Get(TestKey);
			Assert.Equal(TestValue, retrievedValue);
			redisCmsService.Delete(TestKey);
			var deletedValue = redisCmsService.Get(TestKey);
			Assert.Null(deletedValue);
		}
	}
}
