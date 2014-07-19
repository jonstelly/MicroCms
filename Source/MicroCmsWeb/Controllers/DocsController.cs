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
                return View(Cms.GetArea().DocumentRepository.GetAll().Select(d => new ContentTitle(d.Id, d.Title)));

            return View(Cms.GetArea().ContentSearch.SearchDocuments(DocumentField.Title, q).Union(Cms.GetArea().ContentSearch.SearchDocuments(DocumentField.Value, q)));
        }

        public ActionResult Item(Guid id)
        {
            return View(Cms.GetArea().DocumentRepository.Find(id));
        }
    }
}