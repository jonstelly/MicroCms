using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MicroCms.Renderers;
using MicroCms.Search;
using MicroCms.Storage;

namespace MicroCms.Configuration
{
    public interface ICmsConfigurator
    {
        ICmsConfigurator UseDocService<TDocService>(params object[] parameters) where TDocService : ICmsDocumentService;
        ICmsConfigurator UseViewService<TViewService>(params object[] parameters) where TViewService : ICmsViewService;
        ICmsConfigurator UseRenderer<TRenderService>() where TRenderService : ICmsRenderService;
        ICmsConfigurator UseSearch<TSearchService>(params object[] parameters) where TSearchService : ICmsSearchService;
    }
}
