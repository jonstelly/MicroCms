using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using MicroCms.Configuration;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;

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
                _Container.AddNewExtension<OptionalPropertyInjectionExtension>();
            }

            protected override void RegisterType(Type from, Type to, string name = null, params object[] parameters)
            {
                if (parameters.Length > 0)
                    _Container.RegisterType(from, to, name, new ContainerControlledLifetimeManager(), new InjectionConstructor(parameters));
                else
                    _Container.RegisterType(from, to, name, new ContainerControlledLifetimeManager());
            }
            
            private readonly IUnityContainer _Container;
        }

        private class PropertyInjectionBuilderStrategy : BuilderStrategy
        {
            private readonly IUnityContainer _UnityContainer;

            public PropertyInjectionBuilderStrategy(IUnityContainer unityContainer)
            {
                _UnityContainer = unityContainer;
            }

            public override void PreBuildUp(IBuilderContext context)
            {
                if (!context.BuildKey.Type.FullName.StartsWith("Microsoft.Practices"))
                {
                    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(context.BuildKey.Type);

                    foreach (PropertyDescriptor property in properties)
                    {
                        if (_UnityContainer.IsRegistered(property.PropertyType))
                        {
                            property.SetValue(context.Existing, _UnityContainer.Resolve(property.PropertyType));
                        }
                    }
                }
            }
        }

        public class OptionalPropertyInjectionExtension : UnityContainerExtension
        {
            protected override void Initialize()
            {
                Context.Strategies.Add(new PropertyInjectionBuilderStrategy(Container), UnityBuildStage.Initialization);
            }
        }
    }
}
