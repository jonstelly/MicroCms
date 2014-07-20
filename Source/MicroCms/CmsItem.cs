using System;
using System.Collections.Generic;
using System.Text;

namespace MicroCms
{
    public class CmsItem
    {
        public CmsItem(params CmsPart[] parts)
            : this()
        {
            Parts = new List<CmsPart>(parts);
        }

        public CmsItem(object renderAttributes, params CmsPart[] parts)
            : this()
        {
            Parts = new List<CmsPart>(parts);
            RenderAttributes = renderAttributes.ToAttributeDictionary();
        }

        protected CmsItem()
        {
        }

        public virtual List<CmsPart> Parts { get; set; }

        public virtual Dictionary<string, string> RenderAttributes { get; set; }
    }
}
