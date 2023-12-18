using Microsoft.AspNetCore.Mvc;

namespace IS220.O11.HTCL.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
