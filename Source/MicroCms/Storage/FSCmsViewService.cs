using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MicroCms.Storage
{
    public class FSCmsViewService : FSCmsEntityService<CmsView>, ICmsViewService
    {
        public FSCmsViewService(DirectoryInfo baseDirectory)
            : base(new DirectoryInfo(Path.Combine(baseDirectory.FullName, "Views")))
        {
        }
    }
}