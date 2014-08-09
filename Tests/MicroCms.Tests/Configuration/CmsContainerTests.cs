using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Search;
using MicroCms.Storage;
using Xunit;

namespace MicroCms.Tests.Configuration
{
    public abstract class CmsContainerTests : CmsEnvironmentTests
    {
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
