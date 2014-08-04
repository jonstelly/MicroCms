using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace MicroCms.WebApi
{
    public abstract class CmsDocumentsController : ApiController
    {
        [Route("", Name="GetCmsDocumentsApi")]
        [HttpGet]
        public virtual IEnumerable<CmsDocument> Get()
        {
            return Cms.CreateContext().Documents.GetAll().Select(t => new CmsDocument
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
            try
            {
                return Cms.CreateContext().Documents.Find(id);
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Error in Get({0}) - {1}", id, exception);
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [Route("{path}", Name="GetCmsDocumentsByPathApi")]
        [HttpGet]
        public virtual IEnumerable<CmsDocument> GetByPath(string path)
        {
            return Cms.CreateContext().Documents.GetByTag(path).Select(t => new CmsDocument
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

            Cms.CreateContext().Documents.Save(document);
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

            Cms.CreateContext().Documents.Save(document);
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
            Cms.CreateContext().Documents.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
