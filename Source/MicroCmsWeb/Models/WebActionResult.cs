using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroCms.Models
{
    public class WebActionResult
    {
        public const string ActionResultKey = "ActionResult";

        public ResultStatus Status { get; set; }
        public string Message { get; set; }
    }
}