using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Renderers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.Tests.Renderers
{
    [TestClass]
    public class TextCmsRendererServiceTests
    {
        [TestMethod]
        public void ValidateBasicRender()
        {
            var result = new TextCmsRendererService().Render(new CmsPart(CmsTypes.Text, "Hello, World"));
            Assert.IsNotNull(result);
        }
         
    }
}
