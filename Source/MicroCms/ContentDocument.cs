using System;
using System.Collections.Generic;
using System.Text;

namespace MicroCms
{
    public class ContentDocument : ContentEntity
    {
        public ContentDocument(ContentTemplate template, string title, params ContentItem[] items)
            : this()
        {
            TemplateId = template.Id;
            Title = title;
            Items.AddRange(items);
        }

        protected ContentDocument()
        {
            Items = new List<ContentItem>();
        }

        public virtual string Title { get; set; }
        public virtual string Path { get; set; }
        public virtual List<ContentItem> Items { get; set; }
        public virtual Guid TemplateId { get; set; }
    }
}
