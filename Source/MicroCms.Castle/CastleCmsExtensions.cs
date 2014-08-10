using System;
using System.Collections.Generic;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MicroCms.Configuration;

namespace MicroCms.Castle
{
    public static class CastleCmsExtensions
    {
        public static ICmsConfigurator ConfigureCms(this IWindsorContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            
            return new CastleCmsConfigurator(container);
        }

        private class CastleCmsConfigurator : CmsConfigurator
        {
            public CastleCmsConfigurator(IWindsorContainer container)
            {
                _Container = container;
            }

            private readonly IWindsorContainer _Container;

            protected override void RegisterType(Type @from, Type to, string name = null, params object[] parameters)
            {
                var registration = Component.For(@from).ImplementedBy(to);
                if (name != null)
                    registration = registration.Named(name);
                _Container.Register(registration);
            }
        }
    }
}
