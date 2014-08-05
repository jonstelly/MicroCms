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
        public AutofacCmsContainer(IContainer container)
        {
            _Container = container;
        }

        private readonly IContainer _Container;

        public T Resolve<T>()
        {
            return _Container.Resolve<T>();
        }

        public T Resolve<T>(string name)
        {
            return _Container.ResolveNamed<T>(name);
        }

        public object Resolve(Type type)
        {
            return _Container.Resolve(type);
        }

        public object Resolve(Type type, string name)
        {
            return _Container.ResolveNamed(name, type);
        }

        public void Dispose()
        {
            _Container.Dispose();
        }
    }
}
