using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms.Sql.DataAccess
{
	public class Entity
	{
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Contents { get; set; }
    
        public virtual List<EntityTag> EntityTags { get; set; }
	}
}
