using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MicroCms.Storage
{
    public class FSCmsTemplateService : FSCmsEntityService<CmsTemplate>, ICmsTemplateService
    {
        public FSCmsTemplateService(DirectoryInfo baseDirectory)
            : base(new DirectoryInfo(Path.Combine(baseDirectory.FullName, "Templates")))
        {
        }
    }
}