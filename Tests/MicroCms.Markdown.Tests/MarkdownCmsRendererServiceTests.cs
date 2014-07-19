using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.Markdown.Tests
{
    [TestClass]
    public class MarkdownCmsRendererServiceTests
    {
        [TestMethod]
        public void ValidateBasicRender()
        {
            var result = new MarkdownCmsRendererService().Render(new CmsPart(CmsTypes.Markdown, "#Hello, World"));
            Assert.IsNotNull(result);
        }
         
    }
}
