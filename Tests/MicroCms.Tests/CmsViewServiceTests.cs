using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroCms.Configuration;
using MicroCms.Storage;
using Xunit;

namespace MicroCms.Tests
{
    public abstract class CmsViewServiceTests<TService> : CmsUnityTests
        where TService : ICmsViewService
    {
        protected abstract void ConfigureViewService(ICmsConfigurator configurator);

        protected override void SharedConfiguration(ICmsConfigurator configurator)
        {
            base.SharedConfiguration(configurator);
            ConfigureViewService(configurator);
        }

        [Fact]
        public void GetByTagSucceeds()
        {
            using (var context = CreateContext())
            {
                var view = new CmsView("GetByTag");
                view.Tags.Add("GetByTag");
                context.Views.Save(view);
                var byTag = context.Views.GetByTag("GetByTag").SingleOrDefault();
                Assert.NotNull(byTag);
                Assert.Equal(view.Id, byTag.Id);
                Assert.Equal(view.Title, byTag.Title);
            }
        }

        [Fact]
        public void SaveSucceeds()
        {
            using (var context = CreateContext())
            {
                var view = new CmsView("test");
                context.Views.Save(view);
                var loaded = context.Views.Find(view.Id);
                Assert.NotNull(loaded);
                Assert.Equal(view.Title, loaded.Title);
            }
        }

        [Fact]
        public void GetAllSucceeds()
        {
            using (var context = CreateContext())
            {
                context.Views.Save(new CmsView("test"));
                var views = context.Views.GetAll();
                Assert.NotNull(views);
                Assert.Equal(1, views.Count());
            }
        }

        [Fact]
        public void DeleteSucceeds()
        {
            using (var context = CreateContext())
            {
                var view = new CmsView("test");
                context.Views.Save(view);
                var loaded = context.Views.Find(view.Id);
                Assert.NotNull(loaded);
                context.Views.Delete(view.Id);

                AssertThrows(() => context.Views.Find(view.Id));
            }
        }

    }
}