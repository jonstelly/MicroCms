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
        object Resolve(Type type);
        object Resolve(Type type, string name);

        IEnumerable<T> ResolveAll<T>();

        void Teardown(object @object);
    }
}
