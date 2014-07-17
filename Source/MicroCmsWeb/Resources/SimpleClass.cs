using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroCms.Resources
{
    public class SimpleClass
    {
        public SimpleClass()
        {
            _Value = new Lazy<int>(() => new Random(DateTime.Now.Millisecond).Next());
        }

        private Lazy<int> _Value;

        public int Value { get { return _Value.Value; } }
        public string Name { get; private set; }
    }
}