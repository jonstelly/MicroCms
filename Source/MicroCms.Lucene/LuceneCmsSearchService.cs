using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using MicroCms.Search;
using Version = Lucene.Net.Util.Version;

namespace MicroCms.Lucene
{
    public class LuceneCmsSearchService : ICmsSearchService, IDisposable
    {
        public LuceneCmsSearchService(Directory directory)
        {
            _Directory = directory;
            _Analyzer = new StandardAnalyzer(Version.LUCENE_30);
            try
            {
                //Try to open directory.
                using (var rd = IndexReader.Open(directory, true))
                {
                }
            }
            catch (Exception)
            {
                //If open fails, create it
                using (var writer = new IndexWriter(directory, _Analyzer, true, new IndexWriter.MaxFieldLength(1024 * 1024 * 4)))
                {
                    writer.Flush(true, true, true);
                }
            }
        }

        private readonly Directory _Directory;
        private readonly Analyzer _Analyzer;

        public IEnumerable<CmsTitle> SearchDocuments(CmsDocumentField field, string queryText)
        {
            using (var reader = IndexReader.Open(_Directory, true))
            {
                using (var searcher = new IndexSearcher(reader))
                {
                    var parser = new QueryParser(Version.LUCENE_30, field.ToString(), _Analyzer);
                    var query = parser.Parse(queryText);
                    var results = searcher.Search(query, Math.Max(reader.MaxDoc, 10));
                    foreach (var result in results.ScoreDocs)
                    {
                        var doc = searcher.Doc(result.Doc);
                        yield return new CmsTitle(Guid.Parse(doc.Get(CmsDocumentField.Id.ToString())), doc.Get(CmsDocumentField.Title.ToString()));
                    }
                }
            }
        }

        public IEnumerable<CmsTitle> SearchDocuments(string queryText)
        {
            using (var reader = IndexReader.Open(_Directory, true))
            {
                using (var searcher = new IndexSearcher(reader))
                {
                    var parser = new MultiFieldQueryParser(Version.LUCENE_30, new []{CmsDocumentField.Title.ToString(), CmsDocumentField.Value.ToString()}, _Analyzer);
                    var query = parser.Parse(queryText);
                    var results = searcher.Search(query, Math.Max(reader.MaxDoc, 10));
                    foreach (var result in results.ScoreDocs)
                    {
                        var doc = searcher.Doc(result.Doc);
                        yield return new CmsTitle(Guid.Parse(doc.Get(CmsDocumentField.Id.ToString())), doc.Get(CmsDocumentField.Title.ToString()));
                    }
                }
            }
        }

        public IEnumerable<CmsTitle> GetAll()
        {
            using (var reader = DirectoryReader.Open(_Directory, true))
            {
                for (int i = 0; i < reader.MaxDoc; i++)
                {
                    if (reader.IsDeleted(i))
                        continue;
                    CmsTitle title = null;
                    try
                    {
                        var doc = reader.Document(i);
                        title = new CmsTitle(Guid.Parse(doc.Get(CmsDocumentField.Id.ToString())), doc.Get(CmsDocumentField.Title.ToString()));
                    }
                    catch (Exception)
                    {
                    }
                    if (title != null)
                        yield return title;
                }
            }
        }

        public void AddOrUpdateDocuments(params CmsDocument[] documents)
        {
            DeleteDocuments(documents);
            using (var writer = new IndexWriter(_Directory, _Analyzer, false, new IndexWriter.MaxFieldLength(1024 * 1024 * 4)))
            {
                foreach (var document in documents)
                {
                    if (document.Id == Guid.Empty)
                        throw new ArgumentOutOfRangeException("Attempt to index transient document: " + document.Title);

                    var doc = new Document();
                    doc.Add(new Field(CmsDocumentField.Id.ToString(), document.Id.ToString("b"), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    if (!String.IsNullOrEmpty(document.Title))
                        doc.Add(new Field(CmsDocumentField.Title.ToString(), document.Title, Field.Store.YES, Field.Index.ANALYZED));
                    foreach (var tag in document.Tags)
                    {
                        doc.Add(new Field(CmsDocumentField.Tag.ToString(), tag, Field.Store.YES, Field.Index.ANALYZED));
                    }
                    foreach (var partValue in document.Parts.Select(p => p.Value))
                    {
                        if(!String.IsNullOrEmpty(partValue))
                            doc.Add(new Field(CmsDocumentField.Value.ToString(), partValue, Field.Store.NO, Field.Index.ANALYZED));
                    }
                    writer.AddDocument(doc);
                }
                writer.Flush(true, true, true);
            }
        }

        public void DeleteDocuments(params CmsDocument[] documents)
        {
            using (var reader = IndexReader.Open(_Directory, false))
            {
                foreach (var document in documents)
                {
                    reader.DeleteDocuments(new Term(CmsDocumentField.Id.ToString(), document.Id.ToString("b")));
                }
                reader.Flush();
            }
        }

        public void Dispose()
        {
            _Analyzer.Dispose();
        }
    }
}
