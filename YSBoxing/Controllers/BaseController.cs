using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YSBoxing.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult ErrorMessage(string ErrorInfo)
        {
            ViewData["ErrorInfo"] = ErrorInfo;
            return View("Error");
        }

    }
}