using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace MicroCms.Azure
{
    public class AzureCmsViewService : AzureCmsEntityService<CmsView>, ICmsViewService
    {
        public AzureCmsViewService(CloudBlobClient client, string directory = "views", string container = "cms")
            : base(client, directory, container)
        {
        }
    }
}