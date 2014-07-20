using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Azure;
using MicroCms.Configuration;
using Microsoft.WindowsAzure.Storage;

// ReSharper disable once CheckNamespace
namespace MicroCms
{
    public static class AzureConfigurationExtensions
    {
        public static ICmsConfigurator UseAzureStorage(this ICmsConfigurator configurator, string connectionString)
        {
            var account = CloudStorageAccount.Parse(connectionString);
            configurator.Documents = new AzureCmsDocumentService(account.CreateCloudBlobClient());
            configurator.Templates = new AzureCmsTemplateService(account.CreateCloudBlobClient());
            return configurator;
        }
    }
}
