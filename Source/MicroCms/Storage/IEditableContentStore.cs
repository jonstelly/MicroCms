using System;
using System.Collections.Generic;
using System.Text;

namespace MicroCms.Storage
{
    public interface IEditableContentStore : IContentStore
    {
        ContentItem SaveContent(ContentItem item);
        void DeleteContent(ContentItem item);
    }
}
