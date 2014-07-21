using System;
using System.Collections.Generic;
using System.Text;

namespace MicroCms
{
    public class CmsTemplate : CmsEntity
    {
        public CmsTemplate(string title, string value)
        {
            Title = title;
            Value = value;
        }

        public CmsTemplate()
        {
        }

        public string Value { get; set; }
    }
}
