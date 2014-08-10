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

        protected FSCmsEntityService(DirectoryInfo baseDirectory, string subdirectory)
        {
            if (baseDirectory == null)
                throw new ArgumentNullException("baseDirectory");

            BaseDirectory = new DirectoryInfo(Path.Combine(baseDirectory.FullName, subdirectory));
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

        public virtual IEnumerable<CmsTitle> GetAll()
        {
            foreach (var file in BaseDirectory.GetFiles("*.json").OrderBy(f => f.CreationTime))
            {
                var entity = CmsJson.Deserialize<TEntity>(File.ReadAllText(file.FullName));
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
