using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SportZone.Web.Areas.News.Controllers
{
    public class ReportersController : ReportersBaseController
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
