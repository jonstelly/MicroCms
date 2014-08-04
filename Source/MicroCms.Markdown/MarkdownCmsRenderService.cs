using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using MicroCms.Configuration;
using MicroCms.Renderers;

namespace MicroCms.Markdown
{
    [RenderService(CmsTypes.Markdown)]
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

        private string FormatCodeBlock(CmsContext context, MarkdownDeep.Markdown markdown, string source)
        {
            try
            {
                var match = _LanguageExpression.Match(source.Split('\n')[0].Trim());
                if (match.Success)
                {
                    var language = match.Result("${language}");
                    var contentType = "code/" + language;
                    var codeRenderer = context.GetRenderService(contentType);
                    if (codeRenderer != null)
                    {
                        return codeRenderer.Render(context, new CmsPart(contentType, String.Join("\n", source.Split('\n').Skip(1)))).ToHtml().ToHtmlString();
                    }
                }
            }
            catch
            {
            }
            return markdown.Transform(source);
        }

        protected override XElement CreateElement(CmsContext context, CmsPart part)
        {
            var md = new MarkdownDeep.Markdown
            {
                ExtraMode = true,
                SafeMode = false,
                NewWindowForExternalLinks = true,
                FormatCodeBlock = (markdown, s) => FormatCodeBlock(context, markdown, s)
            };
            return Parse(md.Transform(part.Value));
        }
    }
}
