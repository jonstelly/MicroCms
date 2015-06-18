using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms.Sql.DataAccess
{
	public class Tag
	{
		public Guid TagId { get; set; }
		public string TagValue { get; set; }

		public virtual List<EntityTag> EntityTags { get; set; }
	}
}
