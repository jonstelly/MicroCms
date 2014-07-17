using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroCms
{
    public class ContentPart
    {
        public ContentPart()
        {
        }

        public ContentPart(string contentType, string value)
        {
            ContentType = contentType;
            Value = value;
        }

        public string ContentType { get; set; }
        public string Value { get; set; }
    }
}
