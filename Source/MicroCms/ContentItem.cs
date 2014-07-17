using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms
{
    public class ContentItem : IHaveAttributes
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

        public object Attributes { get; set; }
    }
}
