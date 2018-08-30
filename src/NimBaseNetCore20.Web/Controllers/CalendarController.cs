using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PEIIS.Attributes;

namespace PEIIS.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR")]
    [DisplayOrder(7)]
    [DisplayImage("")]
    [TreeViewSettings("small|label pull-right bg-red|3", "small|label pull-right bg-blue|17")]
    public class CalendarController : Controller
    {
        [DisplayActionMenu]
        [DisplayImage("fa fa-calendar")]
        [ScriptAfterPartialView("/js/calendar.min.js")]
        [TreeView("", "label pull-right bg-red", "3")]
        public IActionResult Calendar(bool partial = false)
        {
            if (partial)
                return PartialView();
            else
                return View();
        }
    }
}
