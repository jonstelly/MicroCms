using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MicroCms.Storage
{
    public class FileSystemContentTemplateRepository : FileSystemContentRepository<ContentTemplate>, IContentTemplateRepository
    {
        public FileSystemContentTemplateRepository(DirectoryInfo baseDirectory)
            : base(new DirectoryInfo(Path.Combine(baseDirectory.FullName, "Templates")))
        {
        }
    }
}