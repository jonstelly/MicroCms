using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MicroCms.Configuration;

namespace MicroCms.Autofac
{
    public class AutofacCmsContainer : ICmsContainer
    {
        public AutofacCmsContainer(ILifetimeScope scope)
        {
            _Scope = scope;
        }

        private readonly ILifetimeScope _Scope;

        public T Resolve<T>()
        {
            return _Scope.Resolve<T>();
        }

        public T Resolve<T>(string name)
        {
            return _Scope.ResolveNamed<T>(name);
        }

        public void Dispose()
        {
            _Scope.Dispose();
        }
    }
}
