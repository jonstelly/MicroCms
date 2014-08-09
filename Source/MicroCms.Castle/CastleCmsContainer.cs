using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using MicroCms.Configuration;

namespace MicroCms.Castle
{
    public class CastleCmsContainer : ICmsContainer
    {
        public CastleCmsContainer(IWindsorContainer container)
        {
            _Container = container;
        }

        private readonly IWindsorContainer _Container;

        public T Resolve<T>()
        {
            return _Container.Resolve<T>();
        }

        public T Resolve<T>(string name)
        {
            return _Container.Resolve<T>(name);
        }

        public object Resolve(Type type)
        {
            return _Container.Resolve(type);
        }

        public object Resolve(Type type, string name)
        {
            return _Container.Resolve(name, type);
        }

        public void Dispose()
        {
            _Container.Dispose();
        }
    }
}
