using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms.Sql.DataAccess
{
	public class EntityTag
	{
		public Guid EntityTagId { get; set; }
		public Guid EntityId { get; set; }
		public Guid TagId { get; set; }

		public virtual Entity Entity { get; set; }
		public virtual Tag Tag { get; set; }
	}
}
