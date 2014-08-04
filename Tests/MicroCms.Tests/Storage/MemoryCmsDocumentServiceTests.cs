using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroCms.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.Tests.Storage
{
    [TestClass]
    public class MemoryCmsDocumentServiceTests
    {
        [TestMethod]
        public void GetAllDocumentsSucceeds()
        {
            var service = new MemoryCmsDocumentService();
            var first = new CmsDocument("first", new CmsPart(CmsTypes.Text, "first"));
            var second = new CmsDocument("second", new CmsPart(CmsTypes.Text, "second"));
            service.Save(first);
            service.Save(second);
            var titles = service.GetAll().ToList();
            Assert.IsNotNull(titles);
            Assert.AreEqual(2, titles.Count);
            var firstLoaded = titles.Single(t => t.Id == first.Id);
            var secondLoaded = titles.Single(t => t.Id == second.Id);
            Assert.AreEqual(first.Title, firstLoaded.Title);
            Assert.AreEqual(second.Title, secondLoaded.Title);
        }
         
    }
}
