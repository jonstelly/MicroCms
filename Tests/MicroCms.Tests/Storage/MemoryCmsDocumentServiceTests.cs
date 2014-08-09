using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroCms.Storage;
using Xunit;

namespace MicroCms.Tests.Storage
{
    public class MemoryCmsDocumentServiceTests : CmsUnityTests
    {
        [Fact]
        public void GetAllDocumentsSucceeds()
        {
            using (var context = CreateContext())
            {
                var first = new CmsDocument("first", new CmsPart(CmsTypes.Text, "first"));
                var second = new CmsDocument("second", new CmsPart(CmsTypes.Text, "second"));
                context.Documents.Save(first);
                context.Documents.Save(second);
                var titles = context.Documents.GetAll().ToList();
                Assert.NotNull(titles);
                Assert.Equal(2, titles.Count);
                var firstLoaded = titles.Single(t => t.Id == first.Id);
                var secondLoaded = titles.Single(t => t.Id == second.Id);
                Assert.Equal(first.Title, firstLoaded.Title);
                Assert.Equal(second.Title, secondLoaded.Title);
            }
        }
         
    }
}
