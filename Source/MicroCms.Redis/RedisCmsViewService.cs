using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MicroCms.Storage;

namespace MicroCms.Redis
{
	public class RedisCmsViewService : RedisCmsEntityService<CmsView>, ICmsViewService
	{
		public RedisCmsViewService(string configuration)
			: base(configuration)
		{
		}
	}
}