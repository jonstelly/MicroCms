using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms.Configuration
{
    public class CmsContainerProvider : ICmsContainerProvider
    {
        public CmsContainerProvider(Func<ICmsContainer> creator, bool disposeOnComplete = true)
        {
            _Creator = creator;
            DisposeOnComplete = disposeOnComplete;
        }

        private readonly Func<ICmsContainer> _Creator; 

        public bool DisposeOnComplete { get; private set; }
        public ICmsContainer GetContainer()
        {
            return _Creator();
        }
    }
}
