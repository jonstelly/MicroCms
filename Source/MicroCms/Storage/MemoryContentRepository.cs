using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms.Storage
{
    public abstract class MemoryContentRepository<TEntity> : IContentRepository<TEntity>
        where TEntity : ContentEntity
    {
        private readonly ConcurrentDictionary<Guid, TEntity> _EntitiesById = new ConcurrentDictionary<Guid, TEntity>();
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<Guid, TEntity>> _EntitiesByTag = new ConcurrentDictionary<string, ConcurrentDictionary<Guid, TEntity>>();
 
        public TEntity Find(Guid id)
        {
            return _EntitiesById[id];
        }

        public IEnumerable<TEntity> GetByTag(string tag)
        {
            ConcurrentDictionary<Guid, TEntity> entities;
            if (_EntitiesByTag.TryGetValue(tag, out entities))
            {
                foreach (var entity in entities.Values)
                {
                    yield return entity;
                }
            }
        }

        public void Save(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            
            _EntitiesById[entity.Id] = entity;
            if (entity.Tag != null)
            {
                var tag = _EntitiesByTag.GetOrAdd(entity.Tag, t => new ConcurrentDictionary<Guid, TEntity>());
                tag[entity.Id] = entity;
            }
        }

        public TEntity Delete(Guid id)
        {
            TEntity entity;
            if (!_EntitiesById.TryRemove(id, out entity))
                return null;

            if (!String.IsNullOrEmpty(entity.Tag))
            {
                ConcurrentDictionary<Guid, TEntity> tag;
                if (!_EntitiesByTag.TryGetValue(entity.Tag, out tag))
                    return entity;
                TEntity tagEntity;
                tag.TryRemove(entity.Id, out tagEntity);
            }
            return entity;
        }

        public IEnumerable<TEntity> GetAll()
        {
            foreach (var entity in _EntitiesById.Values)
            {
                yield return entity;
            }
        }
    }
}
