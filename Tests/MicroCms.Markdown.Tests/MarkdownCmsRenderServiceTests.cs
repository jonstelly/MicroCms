using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using MicroCms.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.Markdown.Tests
{
    [TestClass]
    public class MarkdownCmsRenderServiceTests
    {
        [TestMethod]
        public void ValidateHeadingRender()
        {
            var result = new MarkdownCmsRenderService().Render(new CmsPart(CmsTypes.Markdown, "#Hello, World"));
            Assert.IsNotNull(result);
            result.AssertXml("<div><h1>Hello, World</h1></div>");
        }

        [TestMethod]
        public void ValidateHeadingRenderWithAttributes()
        {
            var result = new MarkdownCmsRenderService().Render(new CmsPart(CmsTypes.Markdown, "#Hello, World", new { @class = "example" }));
            Assert.IsNotNull(result);
            result.AssertXml(@"<div class=""example""><h1>Hello, World</h1></div>");
        }

        [TestMethod]
        public void ValidateCodeRender()
        {
            var result = new MarkdownCmsRenderService().Render(new CmsPart(CmsTypes.Markdown, @"
    {{CSharp}}
    public class Thing : IDisposable
    {
        public Thing(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }"));
            Assert.IsNotNull(result);
            result.AssertXml(@"<div style=""color:Black;background-color:White;""><pre>
<span style=""color:Blue;"">public</span> <span style=""color:Blue;"">class</span> Thing : IDisposable
{
    <span style=""color:Blue;"">public</span> Thing(<span style=""color:Blue;"">string</span> name)
    {
        Name = name;
    }

    <span style=""color:Blue;"">public</span> <span style=""color:Blue;"">string</span> Name { <span style=""color:Blue;"">get</span>; <span style=""color:Blue;"">set</span>; }
}

</pre></div>");
        }

        [TestMethod]
        public void ValidateParagraphRender()
        {
            var result = new MarkdownCmsRenderService().Render(new CmsPart(CmsTypes.Markdown, @"Hello, World"));
            result.AssertXml("<div><p>Hello, World</p></div>");
        }

        [TestMethod]
        public void ValidateOrderedListRender()
        {
            var result = new MarkdownCmsRenderService().Render(new CmsPart(CmsTypes.Markdown, @"1. First
2. Second
3. Third"));
            result.AssertXml(@"<div><ol>
<li>First</li>
<li>Second</li>
<li>Third</li>
</ol></div>");
        }

        [TestMethod]
        public void ValidateUnorderedListRender()
        {
            var result = new MarkdownCmsRenderService().Render(new CmsPart(CmsTypes.Markdown, @"* First
* Second
* Third"));
            result.AssertXml(@"<div><ul>
<li>First</li>
<li>Second</li>
<li>Third</li>
</ul></div>");
        }
    }
}
