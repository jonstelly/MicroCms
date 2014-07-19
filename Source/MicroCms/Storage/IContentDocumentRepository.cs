using System;
using System.Collections.Generic;
using System.Text;

namespace MicroCms.Storage
{
    public interface IContentDocumentRepository : IContentRepository<ContentDocument>
    {
        IEnumerable<ContentDocument> GetByPath(string path);
    }
}