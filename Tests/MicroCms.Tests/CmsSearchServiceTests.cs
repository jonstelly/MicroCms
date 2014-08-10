using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroCms.Configuration;
using MicroCms.Search;
using Xunit;

namespace MicroCms.Tests
{
    public abstract class CmsSearchServiceTests : CmsUnityTests
    {
        protected abstract void ConfigureSearchService(ICmsConfigurator configurator);

        protected override void SharedConfiguration(ICmsConfigurator configurator)
        {
            base.SharedConfiguration(configurator);
            ConfigureSearchService(configurator);
        }

        [Fact]
        public void SearchByTitle()
        {
            using (var context = CreateContext())
            {
                var doc = new CmsDocument("SearchByTitle");
                context.Documents.Save(doc);
                var found = context.Search.SearchDocuments(CmsDocumentField.Title, "SearchByTitle").SingleOrDefault();
                Assert.NotNull(found);
                Assert.Equal(doc.Id, found.Id);
                Assert.Equal(doc.Title, found.Title);
            }
        }

        [Fact]
        public void SearchByTag()
        {
            using (var context = CreateContext())
            {
                var doc = new CmsDocument("SearchByTitle");
                doc.Tags.Add("SearchByTag");
                context.Documents.Save(doc);
                var found = context.Search.SearchDocuments(CmsDocumentField.Tag, "SearchByTag").SingleOrDefault();
                Assert.NotNull(found);
                Assert.Equal(doc.Id, found.Id);
                Assert.Equal(doc.Title, found.Title);
            }
        }

        [Fact]
        public void SearchByValue()
        {
            using (var context = CreateContext())
            {
                var doc = new CmsDocument("SearchByTitle", new CmsPart(CmsTypes.Text, "SearchByValue"));
                context.Documents.Save(doc);
                var found = context.Search.SearchDocuments(CmsDocumentField.Value, "SearchByValue").SingleOrDefault();
                Assert.NotNull(found);
                Assert.Equal(doc.Id, found.Id);
                Assert.Equal(doc.Title, found.Title);
            }
        }
    }
}