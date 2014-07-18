using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms
{
    public class ContentItem :  ContentEntity, IHaveAttributes
    {
        public ContentItem(params ContentPart[] parts)
            : this()
        {
            Parts = new List<ContentPart>(parts);
        }

        protected ContentItem()
        {
        }

        public virtual List<ContentPart> Parts { get; set; }

        public virtual object Attributes { get; set; }
    }
}
