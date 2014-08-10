using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Store;
using MicroCms.Configuration;
using MicroCms.Lucene.Configuration;
using MicroCms.Tests;
using Xunit;

namespace MicroCms.Lucene.Tests
{
    public class LuceneCmsSearchServiceTests : CmsSearchServiceTests
    {
        [Fact]
        public void SearchDocumentsSucceeds()
        {
            using (var context = CreateContext())
            {
                var doc = new CmsDocument("Test", new CmsPart(CmsTypes.Text, "Hello, World"));
                context.Documents.Save(doc);
                var found = context.Search.SearchDocuments("World").SingleOrDefault();
                Assert.NotNull(found);
                Assert.Equal(doc.Id, found.Id);
                Assert.Equal(doc.Title, found.Title);
            }
        }

        protected override void ConfigureSearchService(ICmsConfigurator configurator)
        {
            configurator.UseLuceneSearch(new RAMDirectory());
        }
    }
}
