using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using MicroCms.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.Client.Tests
{
    [TestClass]
    public class MicroCmsClientTests
    {
        private static readonly Uri _WebApiUrl = new Uri(Startup.WebUrl, "api/cms/");

        [TestMethod]
        public void GetDocumentsSucceeds()
        {
            using (var client = new MicroCmsClient(_WebApiUrl))
            {
                var documents = client.GetDocumentsAsync().Result;
                Assert.IsNotNull(documents);
                Debug.WriteLine("{0} documents", documents.Length);
                Assert.AreNotEqual(0, documents.Length);
            }
        }

        [TestMethod]
        public void PostDocumentSucceeds()
        {
            using (var client = new MicroCmsClient(_WebApiUrl))
            {
                var document = new CmsDocument("PostedDocument",
                    new CmsPart(CmsTypes.Markdown, "##NEWSTUFF", new
                    {
                        @class = "something"
                    }));

                var url = client.PostDocumentAsync(document).Result;
                
                Assert.IsNotNull(url);

                var loaded = client.GetDocumentAsync(Guid.Parse(url.AbsoluteUri.Split('/').Last())).Result;
                Assert.IsNotNull(loaded);
                Assert.AreEqual(document.Title, loaded.Title);
            }
        }

        [TestMethod]
        public void PutDocumentSucceeds()
        {
            using (var client = new MicroCmsClient(_WebApiUrl))
            {
                var document = CmsTesting.ExampleDocument;
                document.Tags.Add("PutTagAddition");
                client.PutDocumentAsync(document).Wait();

                var loaded = client.GetDocumentAsync(document.Id).Result;
                Assert.IsNotNull(loaded);
                Assert.AreEqual(document.Id, loaded.Id);
                Assert.AreEqual(1, loaded.Tags.Count);
                Assert.AreEqual("PutTagAddition", loaded.Tags.First());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException), "Response status code does not indicate success: 404 (Not Found).")]
        public void DeleteDocumentSucceeds()
        {
            using (var client = new MicroCmsClient(_WebApiUrl))
            {
                var document = new CmsDocument("DeletedDocument", new CmsPart(CmsTypes.Markdown, "##DELETEDSTUFF", new
                {
                    @class = "something"
                }));

                var url = client.PostDocumentAsync(document).Result;
                Assert.IsNotNull(url);

                var id = Guid.Parse(url.AbsoluteUri.Split('/').Last());
                client.DeleteDocumentAsync(id).Wait();

                //Verify that the document was deleted
                try
                {
                    var loaded = client.GetDocumentAsync(id).Result;
                }
                catch (AggregateException exception)
                {
                    throw exception.InnerException;
                }
            }
        }

        [TestMethod]
        public void GetViewsSucceeds()
        {
            using (var client = new MicroCmsClient(_WebApiUrl))
            {
                var views = client.GetViewsAsync().Result;
                Assert.IsNotNull(views);
                Debug.WriteLine("{0} views", views.Length);
                Assert.AreNotEqual(0, views.Length);
            }
        }
    }
}
