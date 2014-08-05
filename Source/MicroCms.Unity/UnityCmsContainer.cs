using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Configuration;
using Microsoft.Practices.Unity;

namespace MicroCms.Unity
{
    public class UnityCmsContainer : ICmsContainer
    {
        public UnityCmsContainer(IUnityContainer container)
        {
            _Container = container;
            _Container.RegisterInstance<ICmsContainer>(this, new ExternallyControlledLifetimeManager());
        }

        private readonly IUnityContainer _Container;

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
            return _Container.Resolve(type, name);
        }

        public void Dispose()
        {
            _Container.Dispose();
        }
    }
}
