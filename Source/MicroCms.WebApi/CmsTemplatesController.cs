using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace MicroCms.WebApi
{
    [RoutePrefix("api/cms/templates")]
    public class CmsTemplatesController : ApiController
    {
        [Route("")]
        [HttpGet]
        public virtual IEnumerable<CmsTemplate> Get()
        {
            return Cms.GetArea().Templates.GetAll().Select(t => new CmsTemplate
            {
                Id = t.Id,
                Title = t.Title,
                Tags = t.Tags
            });
        }

        [Route("{id:guid}", Name = "GetCmsTemplateApi")]
        [HttpGet]
        public virtual CmsTemplate Get(Guid id)
        {
            return Cms.GetArea().Templates.Find(id);
        }
        
        [Route("", Name = "PostCmsTemplateApi")]
        [HttpPost]
        public virtual HttpResponseMessage Post(CmsTemplate template)
        {
            if (template == null)
                throw new ArgumentNullException("template");
            if (template.Id != Guid.Empty)
                throw new ArgumentOutOfRangeException("template", "Attempt to post a non-transient template");

            Cms.GetArea().Templates.Save(template);
            var response = Request.CreateResponse(HttpStatusCode.Created, template);
            string uri = Url.Link("GetCmsTemplateApi", new { id = template.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [Route("{id:guid}", Name = "PutCmsTemplateApi")]
        [HttpPut]
        public virtual HttpResponseMessage Put(Guid id, CmsTemplate template)
        {
            if (id == Guid.Empty)
                throw new ArgumentOutOfRangeException("id", "invalid Template Id");
            if (template == null)
                throw new ArgumentNullException("template");
            if (template.Id != id)
                throw new ArgumentOutOfRangeException("template", "template Id doesn't match id parameter");

            Cms.GetArea().Templates.Save(template);
            var response = Request.CreateResponse(HttpStatusCode.OK, template);
            string uri = Url.Link("GetCmsTemplateApi", new { id = template.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [Route("{id:guid}", Name = "DeleteCmsTemplateApi")]
        [HttpDelete]
        public virtual HttpResponseMessage Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentOutOfRangeException("id", "invalid Template Id");
            Cms.GetArea().Templates.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
