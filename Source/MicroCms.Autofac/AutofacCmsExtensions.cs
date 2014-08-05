using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using MicroCms.Configuration;

namespace MicroCms.Autofac
{
    public static class AutofacCmsExtensions
    {
        public static ICmsConfigurator ConfigureCms(this ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            return new AutofacCmsConfigurator(builder);
        }

        private class AutofacCmsConfigurator : CmsConfigurator
        {
            public AutofacCmsConfigurator(ContainerBuilder builder)
            {
                _Builder = builder;
            }

            private readonly ContainerBuilder _Builder;

            protected override void RegisterType(Type from, Type to, string name = null, params object[] parameters)
            {
                var registration = _Builder.RegisterType(to);
                
                registration = name == null
                    ? registration.As(@from)
                    : registration.Named(name, @from);

                if (parameters.Length > 0)
                {
                    registration.WithParameters(parameters.Select(p => new TypedParameter(p.GetType(), p)));
                }
            }
        }
    }
}
