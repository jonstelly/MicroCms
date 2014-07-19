using System;
using System.Collections.Generic;
using System.Text;

namespace MicroCms
{
    public class CmsDocument : CmsEntity
    {
        public CmsDocument(CmsTemplate template, string title, params CmsItem[] items)
            : this()
        {
            TemplateId = template.Id;
            Title = title;
            Items.AddRange(items);
        }

        protected CmsDocument()
        {
            Items = new List<CmsItem>();
        }

        public virtual string Title { get; set; }
        public virtual string Path { get; set; }
        public virtual List<CmsItem> Items { get; set; }
        public virtual Guid TemplateId { get; set; }
    }
}
