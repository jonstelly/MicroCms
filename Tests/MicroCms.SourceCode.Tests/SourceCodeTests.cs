using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Configuration;
using MicroCms.Tests;
using MicroCms.Unity;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.SourceCode.Tests
{
    [TestClass]
    public class SourceCodeTests
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            var unity = new UnityContainer();
            unity.ConfigureCms()
                .UseMemoryStorage()
                .UseSourceCodeRenderer();
            CmsTesting.Initialize(() => new UnityCmsContainer(unity.CreateChildContainer()));
        }
    }
}
