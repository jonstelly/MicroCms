using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms
{
    public class CmsTitle
    {
        public CmsTitle(Guid id, string title)
        {
            Id = id;
            Title = title;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
    }
}
