using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.Client.Tests
{
    [TestClass]
    public class MicroCmsClientTests
    {
        private static readonly Uri _BaseUrl = new Uri("http://localhost:54981/api/cms/");

        [TestMethod]
        public void GetTemplatesSucceeds()
        {
            using (var client = new MicroCmsClient(_BaseUrl))
            {
                var templates = client.GetTemplatesAsync().Result;
                Assert.IsNotNull(templates);
                Debug.WriteLine("{0} templates", templates.Length);
                Assert.AreNotEqual(0, templates.Length);
            }
        }

        [TestMethod]
        public void GetDocumentsSucceeds()
        {
            using (var client = new MicroCmsClient(_BaseUrl))
            {
                var documents = client.GetDocumentsAsync().Result;
                Assert.IsNotNull(documents);
                Debug.WriteLine("{0} documents", documents.Length);
                Assert.AreNotEqual(0, documents.Length);
            }
        }
    }
}
