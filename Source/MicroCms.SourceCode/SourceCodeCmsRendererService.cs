using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using ColorCode;
using MicroCms.Renderers;

namespace MicroCms.SourceCode
{
    public class SourceCodeCmsRendererService : CmsRendererService
    {
        private readonly ConcurrentDictionary<string, ILanguage> _Languages = new ConcurrentDictionary<string, ILanguage>();

        public const string SourceCodeTypeFamily = "code";

        private ILanguage GetLanguage(string language)
        {
            try
            {
                return _Languages.GetOrAdd(language.ToLowerInvariant(), k =>
                {
                    var languageType = typeof (ILanguage).Assembly.GetType(String.Format("ColorCode.Compilation.Languages.{0}", language), true, true);
                    return (ILanguage)Activator.CreateInstance(languageType);
                });
            }
            catch (Exception)
            {
                throw new ArgumentOutOfRangeException("language", "Unable to find language: " + language);
            }
        }

        protected override XElement CreateElement(CmsPart part)
        {
            return Parse(new CodeColorizer().Colorize(part.Value, GetLanguage(part.ContentSubType)));
        }
    }
}
