using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Storage;
using Microsoft.Data.Edm.Validation;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace MicroCms.Azure
{
    public abstract class AzureCmsEntityService<TEntity> : ICmsEntityService<TEntity>
        where TEntity : CmsEntity
    {
        protected AzureCmsEntityService(CloudBlobClient client, string directory, string container = "cms")
        {
            _Client = client;
            _Container  = _Client.GetContainerReference(container);
            _Container.CreateIfNotExists(BlobContainerPublicAccessType.Off);
            _Directory = _Container.GetDirectoryReference(directory);
        }

        private readonly CloudBlobClient _Client;
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly CloudBlobContainer _Container;
        private readonly CloudBlobDirectory _Directory;

        public TEntity Find(Guid id)
        {
            var blob = GetBlob(id);
            return Read(blob);
        }

        private TEntity Read(CloudBlockBlob blob)
        {
            try
            {
                using (var s = blob.OpenRead())
                {
                    using (var rd = new StreamReader(s, Encoding.UTF8))
                    {
                        var json = rd.ReadToEnd();
                        return CmsJson.Deserialize<TEntity>(json);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual void Save(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();
            var blob = GetBlob(entity.Id);
            using (var s = blob.OpenWrite())
            {
                var json = CmsJson.Serialize(entity);
                var bytes = Encoding.UTF8.GetBytes(json);
                s.Write(bytes, 0, bytes.Length);
            }
        }

        public TEntity Delete(Guid id)
        {
            var blob = GetBlob(id);
            var ret = Read(blob);
            blob.DeleteIfExists();
            return ret;
        }

        public IEnumerable<TEntity> GetAll()
        {
            foreach (var item in _Directory.ListBlobs())
            {
                var blob = _Client.GetBlobReferenceFromServer(item.Uri) as CloudBlockBlob;
                if(blob != null)
                    yield return Read(blob);
            }
        }

        private CloudBlockBlob GetBlob(Guid id)
        {
            return _Directory.GetBlockBlobReference(id + ".json");
        }
    }
}
