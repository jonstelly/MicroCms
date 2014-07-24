using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Configuration;
using MicroCms.Views;

namespace MicroCms.Tests
{
    public static class CmsTests
    {
        public static void Initialize(Action<ICmsConfigurator> configuration = null)
        {
            var area = Cms.Configure(c =>
            {
                c.RegisterBasicRenderServices();
                c.UseMemoryStorage();
                if (configuration != null)
                    configuration(c);
            });
            ExampleView = new CmsContentView("ExampleView", "<div>{0}</div>");
            area.Views.Save(ExampleView);
            ExampleDocument = new CmsDocument("ExampleDocument", new CmsPart(CmsTypes.Markdown, "#Hello, World"));
            area.Documents.Save(ExampleDocument);
        }

        public static CmsView ExampleView { get; private set; }
        public static CmsDocument ExampleDocument { get; private set; }
    }
}
