using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.Markdown.Tests
{
    [TestClass]
    public class MarkdownRendererTests
    {
        [TestMethod]
        public void ValidateBasicRender()
        {
            var result = new MarkdownRenderer().Render(new ContentPart(ContentTypes.Markdown, "#Hello, World"));
            Assert.IsNotNull(result);
        }
         
    }
}
