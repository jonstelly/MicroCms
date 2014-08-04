using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;
using MicroCms.WebApi;

namespace $rootnamespace$.Controllers.CmsApi
{
    [Authorize]
	[RoutePrefix("api/cms/docs")]
    public class DocsController : CmsDocumentsController
    {
    }
}
