using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Store;
using MicroCms.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.Markdown.Tests
{
    [TestClass]
    public class MarkdownTests
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            CmsTests.Initialize(c => c.EnableMarkdownRenderService()
                .EnableSourceCodeRenderService()
                .UseLuceneSearch(new RAMDirectory()));
        }
    }
}
