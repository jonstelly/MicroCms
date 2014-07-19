using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroCms.Search;

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
    }
}