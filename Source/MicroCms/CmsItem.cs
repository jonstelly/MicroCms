using System;
using System.Collections.Generic;
using System.Text;

namespace MicroCms
{
    public class CmsItem :  CmsEntity
    {
        public CmsItem(params CmsPart[] parts)
            : this()
        {
            Parts = new List<CmsPart>(parts);
        }

        public CmsItem(object attributes, params CmsPart[] parts)
            : this()
        {
            Parts = new List<CmsPart>(parts);
            Attributes = attributes.ToAttributeDictionary();
        }

        protected CmsItem()
        {
        }

        public virtual List<CmsPart> Parts { get; set; }

        public virtual Dictionary<string, string> Attributes { get; set; }
    }
}
