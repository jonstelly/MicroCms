using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MicroCms
{
    public interface IContentRenderer
    {
        IHtmlString Render(ContentPart part);
    }
}
