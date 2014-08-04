using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.Tests.Storage
{
    [TestClass]
    public class FSCmsDocumentServiceTests
    {
        private FSCmsDocumentService CreateService()
        {
            return new FSCmsDocumentService(new DirectoryInfo(@".\" + Guid.NewGuid()));
        }

        [TestMethod]
        public void SaveSucceeds()
        {
            var fs = CreateService();
            var doc = new CmsDocument("test");
            fs.Save(doc);
            var loaded = fs.Find(doc.Id);
            Assert.IsNotNull(loaded);
            Assert.AreEqual(doc.Title, loaded.Title);
        }

        [TestMethod]
        public void GetAllSucceeds()
        {
            var fs = CreateService();
            fs.Save(new CmsDocument("test"));
            var docs = fs.GetAll();
            Assert.IsNotNull(docs);
            Assert.AreEqual(1, docs.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void DeleteSucceeds()
        {
            var fs = CreateService();
            var doc = new CmsDocument("test"); 
            fs.Save(doc);
            var loaded = fs.Find(doc.Id);
            Assert.IsNotNull(loaded);
            fs.Delete(doc.Id);
            fs.Find(doc.Id);
        }
    }
}
