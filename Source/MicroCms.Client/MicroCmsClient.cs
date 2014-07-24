using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms.Client
{
    public class MicroCmsClient : IDisposable
    {
        public MicroCmsClient(Uri cmsApiUrl)
        {
            if (cmsApiUrl == null)
                throw new ArgumentNullException("cmsApiUrl");

            _Client = new HttpClient();
            _Client.BaseAddress = cmsApiUrl;
            _Client.DefaultRequestHeaders.Accept.Clear();
            _Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private readonly HttpClient _Client;

        public async Task<CmsDocument> GetDocumentAsync(Guid id)
        {
            Debug.WriteLine("GetDocumentAsync({0})", id);
            return await GetEntityAsync<CmsDocument>(id);
        }

        public async Task<CmsDocument[]> GetDocumentsAsync(string path = null)
        {
            Debug.WriteLine("GetDocumentsAsync({0})", (object)path);
            return await GetEntitiesAsync<CmsDocument>(path);
        }

        public async Task<Uri> PostDocumentAsync(CmsDocument document)
        {
            Debug.WriteLine("PostDocumentAsync({0})", document);
            return await PostEntityAsync(document);
        }

        public async Task PutDocumentAsync(CmsDocument document)
        {
            Debug.WriteLine("PutDocumentAsync({0})", document);
            await PutEntityAsync(document);
        }

        public async Task DeleteDocumentAsync(Guid id)
        {
            Debug.WriteLine("DeleteDocumentAsync({0})", id);
            await DeleteEntityAsync<CmsDocument>(id);
        }

        public async Task<CmsView> GetViewAsync(Guid id)
        {
            Debug.WriteLine("GetViewAsync({0})", id);
            return await GetEntityAsync<CmsView>(id);
        }

        public async Task<CmsView[]> GetViewsAsync(string path = null)
        {
            Debug.WriteLine("GetViewsAsync({0})", (object)path);
            return await GetEntitiesAsync<CmsView>(path);
        }

        public async Task<Uri> PostViewAsync(CmsView View)
        {
            Debug.WriteLine("PostViewAsync({0})", View);
            return await PostEntityAsync(View);
        }

        public async Task PutViewAsync(CmsView View)
        {
            Debug.WriteLine("PutViewAsync({0})", View);
            await PutEntityAsync(View);
        }

        public async Task DeleteViewAsync(Guid id)
        {
            Debug.WriteLine("DeleteViewAsync({0})", id);
            await DeleteEntityAsync<CmsView>(id);
        }

        public void Dispose()
        {
            _Client.Dispose();
        }

        private static readonly Dictionary<Type, string> _EntityTypeAlias = new Dictionary<Type, string>
        {
            {typeof(CmsDocument), "docs"},
            {typeof(CmsView), "Views"}
        };

        protected static JsonMediaTypeFormatter Formatter = new JsonMediaTypeFormatter
        {
            SerializerSettings = CmsJson.Settings
        };

        protected async Task<TEntity> GetEntityAsync<TEntity>(Guid id)
            where TEntity : CmsEntity
        {
            var response = await _Client.GetAsync(GetEntityPath<TEntity>(id));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<TEntity>(new[]
            {
                Formatter
            });
        }

        protected async Task<TEntity[]> GetEntitiesAsync<TEntity>(string path = null)
            where TEntity : CmsEntity
        {
            var response = await _Client.GetAsync(GetEntityPath<TEntity>(path));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<TEntity[]>(new[]
            {
                Formatter
            });
        }

        protected async Task<Uri> PostEntityAsync<TEntity>(TEntity entity)
            where TEntity : CmsEntity
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            if (entity.Id != Guid.Empty)
                throw new ArgumentOutOfRangeException("entity", "Attempt to post non-transient entity");

            var response = await _Client.PostAsync(GetEntityPath<TEntity>(), entity, Formatter);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }

        protected async Task PutEntityAsync<TEntity>(TEntity entity)
            where TEntity : CmsEntity
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            if (entity.Id == Guid.Empty)
                throw new ArgumentOutOfRangeException("entity", "Attempt to put transient entity");

            var response = await _Client.PutAsync(GetEntityPath<TEntity>(entity.Id), entity, Formatter);
            response.EnsureSuccessStatusCode();
        }

        protected async Task DeleteEntityAsync<TEntity>(Guid id)
            where TEntity : CmsEntity
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("id");

            var response = await _Client.DeleteAsync(GetEntityPath<TEntity>(id));
            response.EnsureSuccessStatusCode();
        }

        public static void RegisterTypeAlias<TEntity>(string alias)
            where TEntity : CmsEntity
        {
            _EntityTypeAlias[typeof (TEntity)] = alias;
        }

        protected string GetEntityPath<TEntity>(Guid id)
            where TEntity : CmsEntity
        {
            var alias = _EntityTypeAlias[typeof(TEntity)];
            var ret = String.Format("{0}/{1}", alias, id);
            Debug.WriteLine("GetEntityPath<{0}>({1}) = {2}", typeof(TEntity).Name, id, ret);
            return ret;
        }

        protected string GetEntityPath<TEntity>(string path = null)
            where TEntity : CmsEntity
        {
            var alias = _EntityTypeAlias[typeof(TEntity)];
            var ret = String.IsNullOrEmpty(path) ? alias : String.Format("{0}/{1}", alias, path);
            Debug.WriteLine("GetEntityPath<{0}>({1}) = {2}", typeof (TEntity).Name, path, ret);
            return ret;
        }
    }
}
