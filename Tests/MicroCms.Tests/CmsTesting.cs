using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Configuration;
using MicroCms.Views;

namespace MicroCms.Tests
{
    public static class CmsTesting
    {
        public static void Initialize(Func<ICmsContainer> containerFunc, bool disposeOnComplete = true)
        {
            Cms.Configure(containerFunc, disposeOnComplete);
            using (var context = Cms.CreateContext())
            {
                ExampleView = new CmsContentView("ExampleView", "<div>{0}</div>");
                context.Views.Save(ExampleView);
                ExampleDocument = new CmsDocument("ExampleDocument", new CmsPart(CmsTypes.Markdown, "#Hello, World"));
                context.Documents.Save(ExampleDocument);
            }
        }

        public static CmsView ExampleView { get; private set; }
        public static CmsDocument ExampleDocument { get; private set; }

    }
}
