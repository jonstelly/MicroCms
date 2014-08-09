using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using MicroCms.Configuration;
using MicroCms.Tests;
using Xunit;

namespace MicroCms.Markdown.Tests
{
    public class MarkdownCmsRenderServiceTests : CmsUnityTests
    {
        private void WithSourceCode(ICmsConfigurator configurator)
        {
            configurator
                .UseMarkdownRenderer()
                .UseSourceCodeRenderer();
        }

        private void WithoutSourceCode(ICmsConfigurator configurator)
        {
            configurator
                .UseMarkdownRenderer();
        }

        [Fact]
        public void ValidateHeadingRender()
        {
            using (var context = CreateContext(WithoutSourceCode))
            {
                var result = context.Render(new CmsPart(CmsTypes.Markdown, "#Hello, World"));
                Assert.NotNull(result);
                result.AssertXml("<div><h1>Hello, World</h1></div>");
            }
        }

        [Fact]
        public void ValidateHeadingRenderWithAttributes()
        {
            using (var context = CreateContext(WithoutSourceCode))
            {
                var result = new MarkdownCmsRenderService().Render(context, new CmsPart(CmsTypes.Markdown, "#Hello, World", new
                {
                    @class = "example"
                }));
                Assert.NotNull(result);
                result.AssertXml(@"<div class=""example""><h1>Hello, World</h1></div>");
            }
        }

        [Fact]
        public void ValidateCodeRender()
        {
            using (var context = CreateContext(WithSourceCode))
            {
                var result = new MarkdownCmsRenderService().Render(context, new CmsPart(CmsTypes.Markdown, @"
    {{CSharp}}
    public class Thing : IDisposable
    {
        public Thing(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }"));
                Assert.NotNull(result);
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
        }

        [Fact]
        public void ValidateParagraphRender()
        {
            using (var context = CreateContext(WithoutSourceCode))
            {
                var result = new MarkdownCmsRenderService().Render(context, new CmsPart(CmsTypes.Markdown, @"Hello, World"));
                result.AssertXml("<div><p>Hello, World</p></div>");
            }
        }

        [Fact]
        public void ValidateOrderedListRender()
        {
            using (var context = CreateContext(WithoutSourceCode))
            {
                var result = new MarkdownCmsRenderService().Render(context, new CmsPart(CmsTypes.Markdown, @"1. First
2. Second
3. Third"));
                result.AssertXml(@"<div><ol>
<li>First</li>
<li>Second</li>
<li>Third</li>
</ol></div>");
            }
        }

        [Fact]
        public void ValidateUnorderedListRender()
        {
            using (var context = CreateContext(WithoutSourceCode))
            {
                var result = new MarkdownCmsRenderService().Render(context, new CmsPart(CmsTypes.Markdown, @"* First
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
}
