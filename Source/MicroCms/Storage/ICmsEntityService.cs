using System;
using System.Collections.Generic;
using System.Text;

namespace MicroCms.Storage
{
    public interface ICmsEntityService<TEntity>
        where TEntity : CmsEntity
    {
        TEntity Find(Guid id);
        void Save(TEntity entity);
        TEntity Delete(Guid id);
        IEnumerable<CmsTitle> GetAll();
        IEnumerable<CmsTitle> GetByTag(string tag);
    }
}
