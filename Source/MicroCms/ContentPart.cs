using System;
using System.Collections.Generic;
using System.Linq;
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

        public string ContentFamily { get { return ContentType.Split('/')[0]; } }
        public string ContentSubType
        {
            get
            {
                var ct = ContentType.Split('/');
                if (ct.Length < 2)
                    return null;
                return String.Join("/", ct.Skip(1));
            }
        }

        public string ContentType { get; set; }
        public string Value { get; set; }
    }
}
