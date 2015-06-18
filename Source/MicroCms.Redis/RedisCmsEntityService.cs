using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MicroCms.Storage;

namespace MicroCms.Redis
{
	public abstract class RedisCmsEntityService<TEntity> : ICmsEntityService<TEntity>
		where TEntity : CmsEntity
	{
		protected readonly RedisCmsService RedisCmsService;

		protected RedisCmsEntityService(string connectionString)
		{
			RedisCmsService = new RedisCmsService(connectionString);
		}

		public virtual TEntity Find(Guid id)
		{
			return CmsJson.Deserialize<TEntity>(RedisCmsService.Get(id.ToString()));
		}

		public virtual void Save(TEntity entity)
		{
			if (entity == null)
				throw new ArgumentNullException("entity");

			if (entity.Id == Guid.Empty)
				entity.Id = Guid.NewGuid();

			RedisCmsService.Set(entity.Id.ToString(), CmsJson.Serialize(entity));
		}

		public virtual TEntity Delete(Guid id)
		{
			var entity = Find(id);
			RedisCmsService.Delete(id.ToString());
			return entity;
		}

		public virtual IEnumerable<CmsTitle> GetAll()
		{
			foreach (var key in RedisCmsService.GetAllKeys())
			{
				var entity = CmsJson.Deserialize<TEntity>(RedisCmsService.Get(key));
				var ret = new CmsTitle(entity.Id, entity.Title);
				if (entity.Tags.Count > 0)
					ret.Tags.AddRange(entity.Tags);
				yield return ret;
			}
		}

		public virtual IEnumerable<CmsTitle> GetByTag(string tag)
		{
			return GetAll().Where(e => e.Tags.Any(t => tag.Equals(t, StringComparison.InvariantCultureIgnoreCase)));
		}
	}
}
