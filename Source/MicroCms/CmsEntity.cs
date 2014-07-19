using System;
using System.Collections.Generic;
using System.Text;

namespace MicroCms
{
    public abstract class CmsEntity
    {
        protected CmsEntity()
        {
        }

        public virtual Guid Id { get; set; }
    }
}
