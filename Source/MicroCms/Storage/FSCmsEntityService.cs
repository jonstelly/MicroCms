using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MicroCms.Storage
{
    public abstract class FSCmsEntityService<TEntity> : ICmsEntityService<TEntity>
        where TEntity : CmsEntity
    {
        protected readonly DirectoryInfo BaseDirectory;

        protected FSCmsEntityService(DirectoryInfo baseDirectory)
        {
            if (baseDirectory == null)
                throw new ArgumentNullException("baseDirectory");

            BaseDirectory = baseDirectory;
            if (!BaseDirectory.Exists)
                Directory.CreateDirectory(BaseDirectory.FullName);
        }

        public virtual TEntity Find(Guid id)
        {
            return CmsJson.Deserialize<TEntity>(File.ReadAllText(GetContentFileName(id)));
        }
        
        public virtual void Save(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            
            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            File.WriteAllText(GetContentFileName(entity.Id), CmsJson.Serialize(entity));
        }

        protected string GetContentFileName(Guid id)
        {
            return Path.Combine(BaseDirectory.FullName, id + ".json");
        }

        public virtual TEntity Delete(Guid id)
        {
            var entity = Find(id);
            File.Delete(GetContentFileName(id));
            return entity;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            foreach (var file in BaseDirectory.GetFiles("*.json").OrderBy(f => f.CreationTime))
            {
                yield return CmsJson.Deserialize<TEntity>(File.ReadAllText(file.FullName));
            }
        }
    }
}
