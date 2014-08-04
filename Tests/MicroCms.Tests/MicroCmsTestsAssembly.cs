using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Configuration;
using MicroCms.Renderers;
using MicroCms.Storage;
using MicroCms.Unity;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.Tests
{
    [TestClass]
    public class MicroCmsTestsAssembly
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            var unity = new UnityContainer();
            unity.ConfigureCms()
                .UseDocService<MemoryCmsDocumentService>(new ConcurrentDictionary<Guid, CmsDocument>())
                .UseViewService<MemoryCmsViewService>(new ConcurrentDictionary<Guid, CmsView>())
                .UseRenderer<TextCmsRenderService>()
                .UseRenderer<HtmlCmsRenderService>();

            CmsTesting.Initialize(() => new UnityCmsContainer(unity.CreateChildContainer()));
        }

    }
}
