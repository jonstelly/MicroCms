using System.Data.Entity;

namespace MicroCms.Sql.DataAccess
{
	public interface ISqlCmsDbContext
	{
		DbSet<Entity> Entities { get; set; }
		DbSet<Tag> Tags { get; set; }
		DbSet<EntityTag> EntityTags { get; set; }
		int SaveChanges();
	}
}