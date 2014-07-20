using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.WebApi.Tests
{
    [TestClass]
    public class CmsDocumentsControllerTests
    {
        protected void UsingClient(Action<HttpClient> action)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54981/api/cms/docs/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                action(client);
            }
        }

        protected static JsonMediaTypeFormatter Formatter = new JsonMediaTypeFormatter
        {
            SerializerSettings = CmsJson.Settings
        };

        [TestMethod]
        public void GetAllDocuments()
        {
            UsingClient(c =>
            {
                var response = c.GetAsync("").Result;
                Assert.IsTrue(response.IsSuccessStatusCode);
                var content = response.Content.ReadAsStringAsync().Result;
                var documents = response.Content.ReadAsAsync<CmsDocument[]>(new[]{Formatter}).Result;
                Assert.IsNotNull(documents);
                Assert.AreNotEqual(0, documents.Length);
            });
        }

        [TestMethod]
        public void CreateNewDocument()
        {
            UsingClient(c =>
            {
                var result = c.PostAsync("", new CmsDocument(new CmsTemplate()
                {
                    Id = new Guid("3a3c028e-49e9-4da9-a732-01bb82d6ef08")
                }, "NewDocument", new CmsItem(new CmsPart(CmsTypes.Markdown, "#HELLO, NEW WORLD"))), Formatter).Wait(10000);
                Assert.IsTrue(result);
            });
        }
    }
}
