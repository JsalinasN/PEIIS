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
    [DisplayOrder(0)]
    [DisplayImage("fa fa-dashboard")]
    [TreeView("i", "fa fa-angle-left pull-right", "" )]
    public class DashboardController : Controller
    {
        [DisplayActionMenu]
        [DisplayImage("fa fa-circle-o")]
        [ScriptAfterPartialView("/lib/AdminLTE-2.3.11/dist/js/pages/dashboard.min.js")]
        public IActionResult Dashboardv1(bool partial = false)
        {
            if (partial)
                return PartialView();
            else
                return View();
        }

        [DisplayActionMenu]
        [DisplayImage("fa fa-circle-o")]
        //[ScriptAfterPartialView("/lib/AdminLTE-2.3.11/plugins/chartjs/Chart.min.js,/lib/AdminLTE-2.3.11/dist/js/pages/dashboard2.js")]
        [ScriptAfterPartialView("/lib/AdminLTE-2.3.11/dist/js/pages/dashboard2.min.js")]

        public IActionResult Dashboardv2(bool partial = false)
        {
            if (partial)
                return PartialView();
            else
                return View();
        }    
    }
}
