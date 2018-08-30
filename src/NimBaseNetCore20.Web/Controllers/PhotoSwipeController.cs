using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PEIIS.Attributes;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PEIIS.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR")]
    [DisplayOrder(10)]
    [DisplayImage("")]
    [TreeViewSettings("")]
    public class PhotoSwipeController : Controller
    {
        [DisplayActionMenu]
        [DisplayImage("fa fa-photo")]
        [ScriptAfterPartialView("/js/PhotoSwipe/photoswipe.min.js")]
        [TreeView("", "label pull-right bg-red", "3")]
        public IActionResult PhotoSwipe(bool partial = false)
        {
            if (partial)
                return PartialView();
            else
                return View();
        }
    }
}
