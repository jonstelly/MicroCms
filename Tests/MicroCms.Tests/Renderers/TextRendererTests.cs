using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Renderers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.Tests.Renderers
{
    [TestClass]
    public class TextRendererTests
    {
        [TestMethod]
        public void ValidateBasicRender()
        {
            var result = new TextRenderer().Render(new ContentPart(ContentTypes.Text, "Hello, World"));
            Assert.IsNotNull(result);
        }
         
    }
}
