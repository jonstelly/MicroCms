using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Renderers;
using MicroCms.Search;
using MicroCms.Storage;
using Xunit;

namespace MicroCms.Tests.Configuration
{
    public abstract class CmsContainerTests : CmsEnvironmentTests
    {
        [Fact]
        public void RendererResolveSucceeds()
        {
            using (var context = CreateContext())
            {
                var renderService = context.GetRenderService("text");
                Assert.IsType<TextCmsRenderService>(renderService);
            }
        }

        [Fact]
        public void RendererResolveFails()
        {
            using (var context = CreateContext())
            {
                bool gotException = false;
                try
                {
                    context.GetRenderService("bad-content-type-123");
                }
                catch (Exception exception)
                {
                    gotException = true;
                }
                if(!gotException)
                    throw new InvalidOperationException("GetRenderService() did not throw an exception");
            }
        }

        [Fact]
        public void OptionalPropertiesAreSetIfConfigured()
        {
            using (var context = CreateContext(c => c.UseSearch<InjectedSearchService>()))
            {
                Assert.IsType<InjectedSearchService>(((MemoryCmsDocumentService) context.Documents).Search);
            }
        }

        [Fact]
        public void OptionalPropertiesAreNullIfNotConfigured()
        {
            using (var context = CreateContext())
            {
                Assert.Null(((MemoryCmsDocumentService)context.Documents).Search);
            }
        }

        public class InjectedSearchService : ICmsSearchService
        {
            public virtual void AddOrUpdateDocuments(params CmsDocument[] documents)
            {
                throw new NotImplementedException();
            }

            public virtual void DeleteDocuments(params CmsDocument[] documents)
            {
                throw new NotImplementedException();
            }

            public virtual IEnumerable<CmsTitle> SearchDocuments(string queryText)
            {
                throw new NotImplementedException();
            }

            public virtual IEnumerable<CmsTitle> SearchDocuments(CmsDocumentField field, string queryText)
            {
                throw new NotImplementedException();
            }

            public virtual IEnumerable<CmsTitle> GetAll()
            {
                throw new NotImplementedException();
            }
        }
    }
}
