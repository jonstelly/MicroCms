using System;
using System.Collections.Generic;
using System.Text;
using MicroCms.Configuration;

namespace MicroCms.Tests
{
    public class TestCmsContext : CmsContext
    {
        public TestCmsContext(ICmsContainerProvider containerProvider)
            : base(containerProvider)
        {
        }
    }
}