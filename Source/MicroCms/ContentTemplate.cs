using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms
{
    public class ContentTemplate : ContentEntity
    {
        public ContentTemplate(string value)
        {
            Value = value;
        }

        public ContentTemplate()
        {
        }

        public string Value { get; set; }
    }
}
