using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms
{
    public class ContentDocument : ContentEntity
    {
        public ContentDocument(ContentTemplate template, params ContentItem[] items)
        {
            TemplateId = template.Id;
            Items = new List<ContentItem>(items);
        }

        public virtual List<ContentItem> Items { get; set; }
        public virtual Guid TemplateId { get; set; }
    }
}
