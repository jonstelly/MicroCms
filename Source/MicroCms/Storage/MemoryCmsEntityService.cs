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
        private readonly ConcurrentDictionary<Guid, TEntity> _Entities = new ConcurrentDictionary<Guid, TEntity>();

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

        public virtual IEnumerable<TEntity> GetAll()
        {
            foreach (var entity in _Entities.Values)
            {
                yield return CmsJson.Clone(entity);
            }
        }
    }
}
