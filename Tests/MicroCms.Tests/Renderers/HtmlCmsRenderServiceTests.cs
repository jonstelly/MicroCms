using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Renderers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.Tests.Renderers
{
    [TestClass]
    public class HtmlCmsRenderServiceTests
    {
        [TestMethod]
        public void ValidateBasicRender()
        {
            using (var context = Cms.CreateContext())
            {
                var result = new HtmlCmsRenderService().Render(context, new CmsPart(CmsTypes.Html, "<html><head></head><body><h1>Hello, HTML</h1></body></html>"));
                Assert.IsNotNull(result);
            }
        }
         
    }
}
