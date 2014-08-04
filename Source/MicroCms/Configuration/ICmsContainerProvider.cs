using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms.Configuration
{
    public interface ICmsContainerProvider
    {
        bool DisposeOnComplete { get; }
        ICmsContainer GetContainer();
    }
}
