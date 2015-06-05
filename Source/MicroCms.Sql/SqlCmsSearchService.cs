using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using MicroCms.Search;
using MicroCms.Sql.DataAccess;

namespace MicroCms.Sql
{
    public class SqlCmsSearchService : ICmsSearchService, IDisposable
    {
        public IEnumerable<CmsTitle> SearchDocuments(string queryText)
        {
            return SearchDocuments(new CmsDocumentField[] {CmsDocumentField.Title, CmsDocumentField.Value}, queryText);
        }

        public IEnumerable<CmsTitle> SearchDocuments(CmsDocumentField field, string queryText)
        {
            return SearchDocuments(new CmsDocumentField[] {field}, queryText);
        }

        public IEnumerable<CmsTitle> SearchDocuments(CmsDocumentField[] fields, string queryText)
        {
            var criteriaList = new List<SearchCriteria>();
            foreach (var field in fields)
                criteriaList.Add(new SearchCriteria() {Field = field, QueryText = queryText, IsRequired = false});
            return SearchDocuments(criteriaList);
        }

        public IEnumerable<CmsTitle> SearchDocuments(IEnumerable<SearchCriteria> searchCriteriaSet)
        {
			using (var db = new SqlCmsDbContext())
            {
	            var query = from e in db.Entities
                            where e.Type == SqlCmsDocumentService.ENTITY_TYPE
                            select e;

                var predicate = PredicateBuilder.False<Entity>();
                foreach (var searchCriteria in searchCriteriaSet)
                {
	                var searchCriteriaItem = searchCriteria;
                    switch (searchCriteria.Field)
                    {
                        case CmsDocumentField.Id:
							predicate = (searchCriteriaItem.IsRequired)
								? predicate.And(e => e.Id == Guid.Parse(searchCriteriaItem.QueryText))
								: predicate.Or(e => e.Id == Guid.Parse(searchCriteriaItem.QueryText));
                            break;

                        case CmsDocumentField.Tag:
							predicate = (searchCriteriaItem.IsRequired)
								? predicate.And(e => e.EntityTags.Any(et => et.Tag.TagValue.Equals(searchCriteriaItem.QueryText, StringComparison.InvariantCultureIgnoreCase)))
								: predicate.Or(e => e.EntityTags.Any(et => et.Tag.TagValue.Equals(searchCriteriaItem.QueryText, StringComparison.InvariantCultureIgnoreCase)));
                            break;

                        case CmsDocumentField.Title:
							predicate = (searchCriteriaItem.IsRequired)
								? predicate.And(e => e.Title.ToLower().Contains(searchCriteriaItem.QueryText.ToLower()))
								: predicate.Or(e => e.Title.ToLower().Contains(searchCriteriaItem.QueryText.ToLower()));
                            break;

                        case CmsDocumentField.Value:
							predicate = (searchCriteriaItem.IsRequired)
								? predicate.And(e => e.Contents.ToLower().Contains(searchCriteriaItem.QueryText.ToLower()))
								: predicate.Or(e => e.Contents.ToLower().Contains(searchCriteriaItem.QueryText.ToLower()));
                            break;
                    }
                }
                query = query.AsExpandable().Where(predicate);
                var matchingEntities = query.ToList();

				foreach (var entityEntity in matchingEntities)
                {
                    var title = new CmsTitle(entityEntity.Id, entityEntity.Title);
                    yield return title;
                }
            }
        }

        public IEnumerable<CmsTitle> GetAll()
        {
            return SearchDocuments(new List<SearchCriteria>());
        }

        public void AddOrUpdateDocuments(params CmsDocument[] documents)
        {
            // nothing to do here...
        }

        public void DeleteDocuments(params CmsDocument[] documents)
        {
            // nothing to do here...
        }

        public void Dispose()
        {
            // nothing to do here...
        }
    }
}
