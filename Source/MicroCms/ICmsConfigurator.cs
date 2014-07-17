using System;
using System.Collections.Generic;
using System.Text;

namespace MicroCms
{
    public interface ICmsConfigurator
    {
        ICmsConfigurator RegisterBasicTypes();
        ICmsConfigurator Register(string contentType, IContentRenderer renderer);
        ContentTypes ContentTypes { get; }
    }
}