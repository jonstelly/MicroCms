using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Sql.DataAccess;
using MicroCms.Storage;

namespace MicroCms.Sql
{
    public abstract class SqlCmsEntityService<TEntity> : ICmsEntityService<TEntity>
        where TEntity : CmsEntity
    {
		private readonly ISqlCmsDbContext _sqlCmsDbContext;

		public SqlCmsEntityService(ISqlCmsDbContext sqlCmsDbContext)
		{
			this._sqlCmsDbContext = sqlCmsDbContext;
		}

        public virtual string EntityType { get { return string.Empty; } }

	    public virtual TEntity Find(Guid id)
	    {
		    var entityEntity = _sqlCmsDbContext.Entities.FirstOrDefault(e => e.Id == id);
		    return GetByEntity(entityEntity);
	    }

	    public virtual void Save(TEntity entity)
	    {
		    bool newEntity = false;
		    var entityEntity = _sqlCmsDbContext.Entities.FirstOrDefault(e => e.Id == entity.Id);

		    if (entityEntity == null)
		    {
			    newEntity = true;
			    entity.Id = Guid.NewGuid();
			    entityEntity = new Entity() {Id = entity.Id, EntityTags = new List<EntityTag>()};
		    }

		    // update entity...
		    entityEntity.Title = entity.Title;
		    entityEntity.Type = EntityType;
		    entityEntity.Contents = CmsJson.Serialize(entity);
		    SaveTags(entityEntity, entity);

		    if (newEntity)
			    _sqlCmsDbContext.Entities.Add(entityEntity);

		    _sqlCmsDbContext.SaveChanges();
	    }

	    private void SaveTags(Entity entityEntity, TEntity entity)
        {
			var allTags = _sqlCmsDbContext.Tags.ToList();
            var currentEntityTags = entityEntity.EntityTags.Select(et => et.Tag).ToList();
			var currentEntityTagValues = (currentEntityTags.Any())
				? currentEntityTags.Select(t => t.TagValue.ToLower()).ToList()
				: new List<string>();
			
            var newEntityTags = entity.Tags.Select(s => s.ToLowerInvariant()).ToList();
			var tagsToAdd = newEntityTags.Except(currentEntityTagValues);
            foreach (var tag in tagsToAdd)
            {
                var assignTag = (allTags.Select(t => t.TagValue.ToLower()).Contains(tag.ToLower()))
                    ? allTags.First(t => t.TagValue.ToLower() == tag.ToLower())
                    : new Tag() { TagId = Guid.NewGuid(), TagValue = tag.ToLower() };

                entityEntity.EntityTags.Add(new EntityTag()
                {
                    EntityTagId = Guid.NewGuid(),
                    EntityId = entity.Id,
                    Tag = assignTag
                });
            }
			var tagsToDelete = currentEntityTagValues.Except(newEntityTags);
            var tagEntitiesToDelete = entityEntity.EntityTags.Where(et => tagsToDelete.Contains(et.Tag.TagValue.ToLower()));
			_sqlCmsDbContext.EntityTags.RemoveRange(tagEntitiesToDelete);
        }

	    public virtual TEntity Delete(Guid id)
	    {
		    var entityEntity = _sqlCmsDbContext.Entities.FirstOrDefault(e => e.Id == id);
		    if (entityEntity == null)
			    return null;

		    var cmsEntity = GetByEntity(entityEntity);
		    _sqlCmsDbContext.EntityTags.RemoveRange(entityEntity.EntityTags);
		    _sqlCmsDbContext.Entities.Remove(entityEntity);
		    _sqlCmsDbContext.SaveChanges();
		    return cmsEntity;
	    }

	    public virtual IEnumerable<CmsTitle> GetAll()
	    {
		    var titles = _sqlCmsDbContext.Entities
			    .Where(e => e.Type == SqlCmsDocumentService.ENTITY_TYPE)
			    .Select(s => new {Id = s.Id, Title = s.Title})
				.ToList()
				.Select(title => new CmsTitle(title.Id, title.Title))
			    .AsEnumerable();
		    return titles;
	    }

	    /// <summary>
        /// Gets all entities that have the passed-in tag
        /// </summary>
        public virtual IEnumerable<CmsTitle> GetByTag(string tag)
        {
            return GetByTags(new string[] {tag});
        }

	    /// <summary>
	    /// Gets all entities that have any of the passed-in tags (i.e. tag1 OR tag2 OR tag3...)
	    /// Passing in no tags returns all entities
	    /// </summary>
	    public virtual IEnumerable<CmsTitle> GetByTags(string[] tags)
	    {
		    if (!tags.Any())
			    return Enumerable.Empty<CmsTitle>();

		    tags = tags.Select(s => s.ToLowerInvariant()).ToArray(); // make each tag lowercase to make query case-insensitive
		    var titles = _sqlCmsDbContext.EntityTags
			    .Where(et => et.Entity.Type == SqlCmsDocumentService.ENTITY_TYPE &&
			                 tags.Contains(et.Tag.TagValue.ToLower()))
			    .Select(et => new {Id = et.Entity.Id, Title = et.Entity.Title})
				.ToList()
				.Select(title => new CmsTitle(title.Id, title.Title))
			    .AsEnumerable();
		    return titles;
	    }

	    private TEntity GetByEntity(Entity entityEntity)
        {
            if (entityEntity == null)
                return null;

            var cmsEntity = CmsJson.Deserialize<TEntity>(entityEntity.Contents);
            cmsEntity.Id = entityEntity.Id;
            cmsEntity.Title = entityEntity.Title;

            cmsEntity.Tags.Clear();
            cmsEntity.Tags.AddRange(entityEntity.EntityTags.Select(et => et.Tag.TagValue));

            return cmsEntity;
        }
    }
}
