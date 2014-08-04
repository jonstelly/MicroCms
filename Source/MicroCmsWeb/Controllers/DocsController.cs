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
                return View(Cms.CreateContext().Documents.GetAll().Select(d => new CmsTitle(d.Id, d.Title)));

            return View(Cms.CreateContext().Search.SearchDocuments(q));
        }

        public ActionResult Item(Guid id)
        {
            try
            {
                var document = Cms.CreateContext().Documents.Find(id);
                return View(document);
            }
            catch (Exception exception)
            {
                TempData.ActionFail(String.Format("Failed to load document {0} - {1}", id, exception.Message));
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(Guid id)
        {
            try
            {
                Cms.CreateContext().Documents.Delete(id);
                TempData.ActionOk(String.Format("Document {0} was deleted", id));
            }
            catch (Exception exception)
            {
                TempData.ActionFail(String.Format("Failed to delete document {0} - {1}", id, exception.Message));
            }
            return RedirectToAction("Index");
        }
    }
}