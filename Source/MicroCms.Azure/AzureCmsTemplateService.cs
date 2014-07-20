using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace MicroCms.Azure
{
    public class AzureCmsTemplateService : AzureCmsEntityService<CmsTemplate>, ICmsTemplateService
    {
        public AzureCmsTemplateService(CloudBlobClient client, string directory = "templates", string container = "cms")
            : base(client, directory, container)
        {
        }
    }
}