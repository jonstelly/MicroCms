using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroCms.Markdown.Tests
{
    [TestClass]
    public class MarkdownCmsRendererServiceTests
    {
        [TestMethod]
        public void ValidateBasicRender()
        {
            var result = new MarkdownCmsRendererService().Render(new CmsPart(CmsTypes.Markdown, "#Hello, World"));
            Assert.IsNotNull(result);
        }

        private static readonly Regex _LanguageExpression = new Regex(@"^{{(?<language>.+)}}$", RegexOptions.Compiled);

        [TestMethod]
        public void ParseLanguageLine()
        {
            var match = _LanguageExpression.Match("{{CSharp}}");
            Assert.IsTrue(match.Success);
            Assert.AreEqual("CSharp", match.Result("${language}"));
        }
         
    }
}
