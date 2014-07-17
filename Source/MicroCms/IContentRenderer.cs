﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace MicroCms
{
    public interface IContentRenderer
    {
        XElement Render(ContentPart part);
    }
}
