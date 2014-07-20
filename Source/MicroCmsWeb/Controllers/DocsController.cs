using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using MicroCms.Client;
using MicroCms.Models;
using MicroCms.Search;
using MicroCms.WebApi;
using WebGrease.Configuration;

namespace MicroCms.Controllers
{
    public class DocsController : Controller
    {
        // GET: Docs
        public ActionResult Index(string q)
        {
            if (String.IsNullOrEmpty(q))
                return View(Cms.GetArea().Documents.GetAll().Select(d => new CmsTitle(d.Id, d.Title)));

            return View(Cms.GetArea().Search.SearchDocuments(CmsDocumentField.Title, q).Union(Cms.GetArea().Search.SearchDocuments(CmsDocumentField.Value, q)));
        }

        public ActionResult Item(Guid id)
        {
            return View(Cms.GetArea().Documents.Find(id));
        }

        public ActionResult Delete(Guid id)
        {
            using (var api = new CmsDocumentsController())
            {
                api.Request = new HttpRequestMessage();
                api.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                var response = api.Delete(id);
                try
                {
                    response.EnsureSuccessStatusCode();
                    TempData.ActionOk(String.Format("Document {0} was deleted", id));
                }
                catch (Exception exception)
                {
                    TempData.ActionFail(String.Format("Failed to delete document {0} - {1}", id, exception.Message));
                    throw;
                }
            }
            return RedirectToAction("Index");
        }
    }
}