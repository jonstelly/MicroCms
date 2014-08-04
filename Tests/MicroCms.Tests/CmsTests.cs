using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.Tests
{
    [TestClass]
    public class CmsTests
    {
        [TestMethod]
        public void CreateContextSucceeds()
        {
            using (var context = Cms.CreateContext())
            {
                Assert.IsNotNull(context);
            }
        }
        
        [TestMethod]
        public void RenderSimpleTextPartSucceeds()
        {
            using (var context = Cms.CreateContext())
            {
                var xml = context.Render(new CmsPart(CmsTypes.Text, "Hello"));
                Assert.IsNotNull(xml);
                Assert.AreEqual("<div>Hello</div>", xml.ToString());
            }
        }

        [TestMethod]
        public void RenderViewAndPartsSucceeds()
        {
            using (var context = Cms.CreateContext())
            {
                var xml = context.Render(new CmsContentView("test"), new CmsPart(CmsTypes.Text, "Hello"));
                Assert.IsNotNull(xml);
                Assert.AreEqual("<div>Hello</div>", xml.ToString());
            }
        }
    }
}
