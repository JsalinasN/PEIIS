using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NimBaseNetCore20.Attributes;

namespace NimBaseNetCore20.Controllers
{
    [DisplayOrder(2)]
    [DisplayImage("")]
    [TreeViewSettings("small|label pull-right bg-green|new")]
    public class WidgetsController : Controller
    {
        [DisplayActionMenu]
        [DisplayImage("fa fa-th")]
        [ScriptAfterPartialView("")]
        //[TreeView("", "label pull-right bg-green", "new")]
        public IActionResult Widgets(bool partial = false)
        {
            if (partial)
                return PartialView();
            else
                return View();
        }
    }
}
