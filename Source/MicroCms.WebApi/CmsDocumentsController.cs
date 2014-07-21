using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroCms.WebApi
{
    [RoutePrefix("api/cms/docs")]
    public class CmsDocumentsController : ApiController
    {
        [Route("")]
        [HttpGet]
        public virtual IEnumerable<CmsDocument> Get()
        {
            return Cms.GetArea().Documents.GetAll().Select(t => new CmsDocument
            {
                Id = t.Id,
                Title = t.Title,
                Tags = t.Tags
            });
        }

        [Route("{id:guid}", Name="GetCmsDocumentApi")]
        [HttpGet]
        public virtual CmsDocument Get(Guid id)
        {
            return Cms.GetArea().Documents.Find(id);
        }

        [Route("{path}")]
        [HttpGet]
        public virtual IEnumerable<CmsDocument> GetByPath(string path)
        {
            return Cms.GetArea().Documents.GetByTag(path).Select(t => new CmsDocument
            {
                Id = t.Id,
                Title = t.Title,
                Tags = t.Tags
            });
        }
        
        [Route("", Name="PostCmsDocumentApi")]
        [HttpPost]
        public virtual HttpResponseMessage Post(CmsDocument document)
        {
            if (document == null)
                throw new ArgumentNullException("document");
            if (document.Id != Guid.Empty)
                throw new ArgumentOutOfRangeException("document", "Attempt to post a non-transient document");

            Cms.GetArea().Documents.Save(document);
            var response = Request.CreateResponse(HttpStatusCode.Created, document);
            string uri = Url.Link("GetCmsDocumentApi", new { id = document.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [Route("{id:guid}", Name = "PutCmsDocumentApi")]
        [HttpPut]
        public virtual HttpResponseMessage Put(Guid id, CmsDocument document)
        {
            if (id == Guid.Empty)
                throw new ArgumentOutOfRangeException("id", "invalid Document Id");
            if (document == null)
                throw new ArgumentNullException("document");         
            if (document.Id != id)
                throw new ArgumentOutOfRangeException("document", "document Id doesn't match id parameter");

            Cms.GetArea().Documents.Save(document);
            var response = Request.CreateResponse(HttpStatusCode.OK, document);
            string uri = Url.Link("GetCmsDocumentApi", new { id = document.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [Route("{id:guid}", Name = "DeleteCmsDocumentApi")]
        [HttpDelete]
        public virtual HttpResponseMessage Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentOutOfRangeException("id", "invalid Document Id");
            Cms.GetArea().Documents.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
