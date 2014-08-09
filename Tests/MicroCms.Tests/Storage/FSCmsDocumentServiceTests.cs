using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Configuration;
using MicroCms.Storage;
using Xunit;

namespace MicroCms.Tests.Storage
{
    public class FSCmsDocumentServiceTests : CmsUnityTests
    {
        private readonly DirectoryInfo _Directory = new DirectoryInfo(@".\" + Guid.NewGuid());

        protected override void SharedConfiguration(ICmsConfigurator configurator)
        {
            base.SharedConfiguration(configurator);
            configurator.UseFileSystemStorage(_Directory);
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
                Assert.Throws<FileNotFoundException>(() => context.Documents.Find(doc.Id));
            }
        }
    }
}
