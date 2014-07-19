using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms.Search
{
    public enum DocumentField
    {
        Id,
        Title,
        Path,
        Value
    }

    public interface IContentSearch
    {
        void AddOrUpdateDocuments(params ContentDocument[] documents);
        IEnumerable<ContentTitle> SearchDocuments(DocumentField field, string queryText);
        void DeleteDocuments(params ContentDocument[] documents);
    }
}
