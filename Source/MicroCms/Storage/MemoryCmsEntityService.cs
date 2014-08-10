using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms.Storage
{
    public abstract class MemoryCmsEntityService<TEntity> : ICmsEntityService<TEntity>
        where TEntity : CmsEntity
    {
        protected MemoryCmsEntityService(ConcurrentDictionary<Guid, TEntity> entities)
        {
            _Entities = entities ?? new ConcurrentDictionary<Guid, TEntity>();
        }

        private readonly ConcurrentDictionary<Guid, TEntity> _Entities;

        public virtual TEntity Find(Guid id)
        {
            return CmsJson.Clone(_Entities[id]);
        }

        public virtual void Save(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();
            _Entities[entity.Id] = CmsJson.Clone(entity);
        }

        public virtual TEntity Delete(Guid id)
        {
            TEntity entity;
            if (!_Entities.TryRemove(id, out entity))
                return null;

            return entity;
        }

        public virtual IEnumerable<CmsTitle> GetAll()
        {
            return _Entities.Values.Select(entity =>
            {
                var ret = new CmsTitle(entity.Id, entity.Title);
                if (entity.Tags.Count > 0)
                    ret.Tags.AddRange(entity.Tags);
                return ret;
            });
        }

        public virtual IEnumerable<CmsTitle> GetByTag(string tag)
        {
            return GetAll().Where(e => e.Tags.Any(t => tag.Equals(t, StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}
