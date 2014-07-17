using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ColorCode;
using ColorCode.Compilation.Languages;

namespace MicroCms
{
    public class SourceCodeRenderer : IContentRenderer
    {
        public const string ContentType = "code";

        private ConcurrentDictionary<string, ILanguage> _Languages = new ConcurrentDictionary<string, ILanguage>();
        
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

        private static readonly CodeColorizer _Colorizer = new CodeColorizer();

        public IHtmlString Render(ContentPart part)
        {
            return new HtmlString(_Colorizer.Colorize(part.Value, GetLanguage(part.ContentSubType)));
        }
    }
}
