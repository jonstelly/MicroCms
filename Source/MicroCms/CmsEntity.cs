using System;
using System.Collections.Generic;
using System.Text;

namespace MicroCms
{
    public abstract class CmsEntity
    {
        protected CmsEntity()
        {
            Tags = new List<string>();
        }

        public virtual Guid Id { get; set; }

        public string Title { get; set; }

        public List<string> Tags { get; set; }
    }
}
