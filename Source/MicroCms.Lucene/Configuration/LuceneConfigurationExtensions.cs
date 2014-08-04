using System;
using System.Collections.Generic;
using System.Text;
using Lucene.Net.Store;
using MicroCms.Configuration;

namespace MicroCms.Lucene.Configuration
{
    public static class LuceneConfigurationExtensions
    {
        public static ICmsConfigurator UseLuceneSearch(this ICmsConfigurator configurator, Directory directory)
        {
            return configurator.UseSearch<LuceneCmsSearchService>(directory);
        }
    }
}
