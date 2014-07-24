using System;
using System.Collections.Generic;
using System.Text;

namespace MicroCms
{
    public class CmsDocument : CmsEntity
    {
        public CmsDocument(string title, params CmsPart[] parts)
            : this()
        {
            Title = title;
            Parts.AddRange(parts);
        }
        
        public CmsDocument()
        {
            Parts = new List<CmsPart>();
        }

        public virtual List<CmsPart> Parts { get; set; }
    }
}
