using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms
{
    public class ContentItem
    {
        public ContentItem()
        {
        }

        public ContentItem(params ContentPart[] parts)
        {
            Parts = new List<ContentPart>(parts);
        }

        public Guid Id { get; set; }

        public List<ContentPart> Parts { get; set; }
    }
}
