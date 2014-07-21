using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms
{
    public class CmsTitle : CmsEntity
    {
        public CmsTitle(Guid id, string title)
        {
            Id = id;
            Title = title;
        }

        protected CmsTitle()
        {
        }
    }
}
