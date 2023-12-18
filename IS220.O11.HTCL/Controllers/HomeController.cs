using IS220.O11.HTCL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace IS220.O11.HTCL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int page)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            if (page == -1)
            {
                ViewBag.flashsale = context.FlashSales();
                ViewBag.listbook = context.GetBook(0);
                ViewBag.page = 1;
            }
            else
            {
                ViewBag.flashsale = context.FlashSales();
                ViewBag.listbook = context.GetBook(page - 1);
                ViewBag.page = page;
            }
            var res = HttpContext.Session.GetString("UserSession");
            if (res != null)
            {
                account usersession = JsonSerializer.Deserialize<account>(res);
                usersession = context.Login(usersession.Tentk, usersession.Matkhau);
                ViewBag.infor = usersession;
                ViewBag.status = "Success";
                ViewBag.avatar = HttpContext.Session.GetString("Avatar");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
