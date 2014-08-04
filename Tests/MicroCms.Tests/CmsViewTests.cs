using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroCms.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.Tests
{
    [TestClass]
    public class CmsViewTests
    {
        [TestMethod]
        public void RenderSucceeds()
        {
            using (var context = Cms.CreateContext())
            {
                var view = new CmsContentView("example");
                var xml = view.Render(context, new CmsDocument("doc", new CmsPart(CmsTypes.Text, "Hello"))).Single();
                Assert.IsNotNull(xml);
                Assert.AreEqual("<div>Hello</div>", xml.ToString());
            }
        }
    }
}
