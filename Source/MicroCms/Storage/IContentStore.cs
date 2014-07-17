using System;
using System.Collections.Generic;
using System.Text;

namespace MicroCms.Storage
{
    public interface IContentStore
    {
        ContentItem GetContent(string path);
    }
}
