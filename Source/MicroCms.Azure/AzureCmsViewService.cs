using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace MicroCms.Azure
{
    public class AzureCmsViewService : AzureCmsEntityService<CmsView>, ICmsViewService
    {
        public AzureCmsViewService(CloudBlobClient client)
            : this(client, "cms", "views")
        {
        }

        public AzureCmsViewService(CloudBlobClient client, string containerName)
            : this(client, containerName, "views")
        {
        }

        public AzureCmsViewService(CloudBlobClient client, string containerName, string directoryName)
            : base(client, containerName, directoryName)
        {
        }
    }
}