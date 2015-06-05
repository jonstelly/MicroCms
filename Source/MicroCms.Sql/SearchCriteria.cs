using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Search;

namespace MicroCms.Sql
{
	public class SearchCriteria
	{
		public CmsDocumentField Field { get; set; }
		public string QueryText { get; set; }
		public bool IsRequired { get; set; }
	}
}
