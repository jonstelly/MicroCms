using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroCms
{
    public class CmsPart
    {
        public CmsPart(string contentType, string value, object renderAttributes = null)
        {
            if (String.IsNullOrEmpty(contentType))
                throw new ArgumentNullException("contentType");
                        
            ContentType = contentType;
            Value = value;
            if (renderAttributes != null)
                RenderAttributes = renderAttributes.ToAttributeDictionary();
        }

        protected CmsPart()
        {
        }

        public Dictionary<string, string> RenderAttributes { get; set; }

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
