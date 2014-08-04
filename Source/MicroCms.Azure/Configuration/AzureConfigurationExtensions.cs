using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;

namespace MicroCms.Azure.Configuration
{
    public static class AzureConfigurationExtensions
    {
        public static ICmsConfigurator UseAzureStorage(this ICmsConfigurator configurator, CloudBlobClient blobClient, string containerName = null)
        {
            configurator.UseDocService<AzureCmsDocumentService>(blobClient, containerName);
            configurator.UseViewService<AzureCmsViewService>(blobClient, containerName);
            return configurator;
        }
    }
}
