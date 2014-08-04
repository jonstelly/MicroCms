using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Configuration;
using Microsoft.Practices.Unity;

// ReSharper disable once CheckNamespace
namespace MicroCms
{
    public static class UnityCmsExtensions
    {
        public static ICmsConfigurator ConfigureCms(this IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            
            return new UnityCmsConfigurator(container);
        }

        private class UnityCmsConfigurator : CmsConfigurator
        {
            public UnityCmsConfigurator(IUnityContainer container)
            {
                _Container = container;
                RegisterContext();
            }
            
            protected override void RegisterType(Type from, Type to, string name = null, params object[] parameters)
            {
                if(parameters.Length > 0)
                    _Container.RegisterType(from, to, name, new ContainerControlledLifetimeManager(), new InjectionConstructor(parameters));
                else
                    _Container.RegisterType(from, to, name, new ContainerControlledLifetimeManager());
            }
            
            private readonly IUnityContainer _Container;
        }
    }
}
