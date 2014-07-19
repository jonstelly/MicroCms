using System;
using System.Collections.Generic;
using System.Text;

namespace MicroCms
{
    public abstract class ContentEntity
    {
        protected ContentEntity()
        {
        }

        public virtual Guid Id { get; set; }
    }
}
