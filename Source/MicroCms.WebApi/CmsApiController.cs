using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;

namespace MicroCms.WebApi
{
    public abstract class CmsApiController : ApiController
    {
        protected CmsContext CmsContext { get { return Request.GetCmsContext(); } }
    }
}