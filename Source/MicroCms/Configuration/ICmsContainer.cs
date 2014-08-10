using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms.Configuration
{
    public interface ICmsContainer : IDisposable
    {
        T Resolve<T>();
        T Resolve<T>(string name);
    }
}
