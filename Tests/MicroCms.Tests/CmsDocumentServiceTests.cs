using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MicroCms.Configuration;
using MicroCms.Storage;
using Xunit;

namespace MicroCms.Tests
{
    public abstract class CmsDocumentServiceTests<TService> : CmsUnityTests
        where TService : ICmsDocumentService
    {
        protected abstract void ConfigureDocumentService(ICmsConfigurator configurator);

        protected override void SharedConfiguration(ICmsConfigurator configurator)
        {
            base.SharedConfiguration(configurator);
            ConfigureDocumentService(configurator);
        }

        [Fact]
        public void GetByTagSucceeds()
        {
            using (var context = CreateContext())
            {
                var doc = new CmsDocument("GetByTag");
                doc.Tags.Add("GetByTag");
                context.Documents.Save(doc);
                var byTag = context.Documents.GetByTag("GetByTag").SingleOrDefault();
                Assert.NotNull(byTag);
                Assert.Equal(doc.Id, byTag.Id);
                Assert.Equal(doc.Title, byTag.Title);
            }
        }

        [Fact]
        public void SaveSucceeds()
        {
            using (var context = CreateContext())
            {
                var doc = new CmsDocument("test");
                context.Documents.Save(doc);
                var loaded = context.Documents.Find(doc.Id);
                Assert.NotNull(loaded);
                Assert.Equal(doc.Title, loaded.Title);
            }
        }

        [Fact]
        public void GetAllSucceeds()
        {
            using (var context = CreateContext())
            {
                context.Documents.Save(new CmsDocument("test"));
                var docs = context.Documents.GetAll();
                Assert.NotNull(docs);
                Assert.Equal(1, docs.Count());
            }
        }

        [Fact]
        public void DeleteSucceeds()
        {
            using (var context = CreateContext())
            {
                var doc = new CmsDocument("test");
                context.Documents.Save(doc);
                var loaded = context.Documents.Find(doc.Id);
                Assert.NotNull(loaded);
                context.Documents.Delete(doc.Id);

                AssertThrows(() => context.Documents.Find(doc.Id));
            }
        }
    }
}