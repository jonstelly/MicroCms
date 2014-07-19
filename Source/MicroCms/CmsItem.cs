using System;
using System.Collections.Generic;
using System.Text;

namespace MicroCms
{
    public class CmsItem :  CmsEntity, IHaveAttributes
    {
        public CmsItem(params CmsPart[] parts)
            : this()
        {
            Parts = new List<CmsPart>(parts);
        }

        protected CmsItem()
        {
        }

        public virtual List<CmsPart> Parts { get; set; }

        public virtual object Attributes { get; set; }
    }
}
