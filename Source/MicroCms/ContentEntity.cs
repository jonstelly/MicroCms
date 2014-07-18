using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms
{
    public abstract class ContentEntity
    {
        protected ContentEntity()
        {
            Id = Guid.NewGuid();
        }

        public virtual Guid Id { get; set; }

        public virtual string Tag { get; set; }
    }
}
