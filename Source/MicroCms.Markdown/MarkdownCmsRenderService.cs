using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using MicroCms.Renderers;

namespace MicroCms.Markdown
{
    public class MarkdownCmsRenderService : CmsRenderService
    {
        public const string ContentType = "markdown";

        public override bool Supports(string contentType)
        {
            if (String.IsNullOrEmpty(contentType))
                return false;
            return contentType.Equals(ContentType, StringComparison.InvariantCultureIgnoreCase);
        }

        private static readonly Regex _LanguageExpression = new Regex(@"^{{(?<language>.+)}}$", RegexOptions.Compiled);

        private static string FormatCodeBlock(MarkdownDeep.Markdown markdown, string source)
        {
            //If code content type is registered
            if (Cms.GetArea().Types.Registered.Any(r => r.ToLowerInvariant().Equals("code")))
            {
                var codeRenderService = Cms.GetArea().Types.GetRenderService("code");
                if (codeRenderService != null)
                {
                    var match = _LanguageExpression.Match(source.Split('\n')[0].Trim());
                    if (match.Success)
                    {
                        var language = match.Result("${language}");
                        if (codeRenderService.Supports("code/" + language))
                        {
                            return codeRenderService.Render(new CmsPart("code/" + language, String.Join("\n", source.Split('\n').Skip(1)))).ToHtml().ToHtmlString();
                        }
                    }
                }
            }
            return markdown.Transform(source);
        }

        protected override XElement CreateElement(CmsPart part)
        {
            var md = new MarkdownDeep.Markdown
            {
                ExtraMode = true,
                SafeMode = false,
                NewWindowForExternalLinks = true,
                FormatCodeBlock = FormatCodeBlock
            };
            return Parse(md.Transform(part.Value));
        }
    }
}
