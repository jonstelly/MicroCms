using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MicroCms.Models
{
    public static class WebActionResultExtensions
    {
        public static WebActionResult ActionFail(this TempDataDictionary dictionary, string message)
        {
            return dictionary.SetActionResult(ResultStatus.Error, message);
        }

        public static WebActionResult ActionWarn(this TempDataDictionary dictionary, string message)
        {
            return dictionary.SetActionResult(ResultStatus.Warning, message);
        }

        public static WebActionResult ActionOk(this TempDataDictionary dictionary, string message)
        {
            return dictionary.SetActionResult(ResultStatus.Success, message);
        }

        public static WebActionResult SetActionResult(this TempDataDictionary dictionary, ResultStatus status, string message)
        {
            var ret = new WebActionResult
            {
                Status = status,
                Message = message
            };
            dictionary[WebActionResult.ActionResultKey] = ret;
            return ret;
        }

        public static IHtmlString RenderActionResult(this TempDataDictionary dictionary)
        {
            object result;
            if (!dictionary.TryGetValue(WebActionResult.ActionResultKey, out result))
                return new HtmlString(String.Empty);

            var actionResult = result as WebActionResult;
            if (actionResult == null)
                return new HtmlString(String.Empty);

            return new HtmlString(String.Format("<div class=\"{0}\">{1}</div><br/>", GetResultCssClass(actionResult.Status), actionResult.Message));
        }

        private static string GetResultCssClass(ResultStatus status)
        {
            switch (status)
            {
                case ResultStatus.Success:
                    return "alert alert-success";
                case ResultStatus.Warning:
                    return "alert alert-warning";
                case ResultStatus.Error:
                    return "alert alert-danger alert-error";
                default:
                    throw new ArgumentOutOfRangeException("status", "Unexpected ResulStatus: " + status);
            }
        }
    }
}