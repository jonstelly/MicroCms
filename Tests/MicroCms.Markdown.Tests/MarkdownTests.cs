using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Store;
using MicroCms.Configuration;
using MicroCms.Storage;
using MicroCms.Tests;
using MicroCms.Unity;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.Markdown.Tests
{
    [TestClass]
    public class MarkdownTests
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            var unity = new UnityContainer();
            unity.ConfigureCms()
                .UseMemoryStorage()
                .UseSourceCodeRenderer()
                .UseMarkdownRenderer();
            CmsTesting.Initialize(() => new UnityCmsContainer(unity.CreateChildContainer()));
        }
    }
}
