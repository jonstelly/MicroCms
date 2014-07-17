using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms
{
    public class CmsConfiguration
    {
        public CmsConfiguration(ContentTypes contentTypes)
        {
            if (contentTypes == null)
                throw new ArgumentNullException("contentTypes");
            
            ContentTypes = contentTypes;
        }

        public ContentTypes ContentTypes { get; private set; }
    }
}
