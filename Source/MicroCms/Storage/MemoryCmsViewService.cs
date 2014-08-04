using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace MicroCms.Storage
{
    public class MemoryCmsViewService : MemoryCmsEntityService<CmsView>, ICmsViewService
    {
        public MemoryCmsViewService()
            : this(new ConcurrentDictionary<Guid, CmsView>())
        {
        }

        public MemoryCmsViewService(ConcurrentDictionary<Guid, CmsView> entities)
            : base(entities)
        {
        }
    }
}