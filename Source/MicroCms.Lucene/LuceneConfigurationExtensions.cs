using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Store;
using MicroCms.Configuration;
using MicroCms.Lucene;

// ReSharper disable once CheckNamespace
namespace MicroCms
{
    public static class LuceneConfigurationExtensions
    {
        public static ICmsConfigurator UseLuceneSearch(this ICmsConfigurator configurator, Directory directory)
        {
            configurator.Search = new LuceneCmsSearchService(directory);
            return configurator;
        }
    }
}
