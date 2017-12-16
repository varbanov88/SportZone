using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportZone.Web.Infrastructure;

namespace SportZone.Web.Areas.News.Controllers
{
    [Area("News")]
    [Authorize(Roles = "Administrator, Reporter")]
    public class ReportersBaseController : Controller
    {
    }
}
