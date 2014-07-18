using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using MicroCms;

namespace MicroCms.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Documents()
        {
            return View(Cms.GetArea().DocumentRepository.GetAll());
        }


        public ActionResult Items()
        {
            return View(new[]
            {
                new ContentItem(new ContentPart(ContentTypes.Markdown, MicroCms.Content.SimpleExample)
                {
                    Attributes = new { @class = "col-md-4" }
                }),
                new ContentItem(
                    new ContentPart(ContentTypes.Markdown, MicroCms.Content.SimpleExample),
                    new ContentPart(ContentTypes.SourceCode + "/csharp", MicroCms.Content.SimpleClass),
                    new ContentPart(ContentTypes.Markdown, MicroCms.Content.SimpleExample),
                    new ContentPart(ContentTypes.SourceCode +"/javascript", MicroCms.Content.SimpleScript))
                    {
                        Attributes = new { @class = "col-md-8" }
                    }
            });
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}