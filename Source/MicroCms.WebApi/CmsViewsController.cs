using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace MicroCms.WebApi
{
    public abstract class CmsViewsController : ApiController
    {
        [Route("", Name="GetCmsViewsApi")]
        [HttpGet]
        public virtual IEnumerable<CmsView> Get()
        {
            return Cms.GetArea().Views.GetAll().Select(t => Cms.GetArea().Views.Find(t.Id));
        }

        [Route("{id:guid}", Name = "GetCmsViewApi")]
        [HttpGet]
        public virtual CmsView Get(Guid id)
        {
            return Cms.GetArea().Views.Find(id);
        }
        
        [Route("", Name = "PostCmsViewApi")]
        [HttpPost]
        public virtual HttpResponseMessage Post(CmsView view)
        {
            if (view == null)
                throw new ArgumentNullException("view");
            if (view.Id != Guid.Empty)
                throw new ArgumentOutOfRangeException("view", "Attempt to post a non-transient view");

            Cms.GetArea().Views.Save(view);
            var response = Request.CreateResponse(HttpStatusCode.Created, view);
            string uri = Url.Link("GetCmsViewApi", new { id = view.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [Route("{id:guid}", Name = "PutCmsViewApi")]
        [HttpPut]
        public virtual HttpResponseMessage Put(Guid id, CmsView view)
        {
            if (id == Guid.Empty)
                throw new ArgumentOutOfRangeException("id", "invalid View Id");
            if (view == null)
                throw new ArgumentNullException("view");
            if (view.Id != id)
                throw new ArgumentOutOfRangeException("view", "view Id doesn't match id parameter");

            Cms.GetArea().Views.Save(view);
            var response = Request.CreateResponse(HttpStatusCode.OK, view);
            string uri = Url.Link("GetCmsViewApi", new { id = view.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [Route("{id:guid}", Name = "DeleteCmsViewApi")]
        [HttpDelete]
        public virtual HttpResponseMessage Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentOutOfRangeException("id", "invalid View Id");
            Cms.GetArea().Views.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
