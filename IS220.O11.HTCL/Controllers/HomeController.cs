using IS220.O11.HTCL.Areas.Admin.Models;
using IS220.O11.HTCL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Http;


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
                ViewBag.listbook = context.GetBook(page);
                ViewBag.page = page;
            }
            var res = HttpContext.Session.GetString("UserSession");
            if (res != null)
            {
                account usersession = JsonSerializer.Deserialize<account>(res);
                usersession = context.Login(usersession.Email, usersession.Matkhau);
                ViewBag.infor = usersession;
                ViewBag.status = "Success";
                ViewBag.avatar = HttpContext.Session.GetString("Avatar");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        public IActionResult Login()
        {
            string email = HttpContext.Request.Form["email"];
            string password = HttpContext.Request.Form["password"];

            StoreContextAdmin adminContext = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Areas.Admin.Models.StoreContextAdmin)) as StoreContextAdmin;
            StoreContext clientContext = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;

            //Try to log in as Admin
            account adminResult = adminContext.login(email, password);
            if (adminResult != null)
            {
                // Admin logged in
                ViewBag.status = "Success";
                ViewBag.infor = adminResult;
                HttpContext.Session.SetString("UserSession", JsonSerializer.Serialize(adminResult));
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            // If not an admin, try to log in as User
            account clientResult = clientContext.Login(email, password);
            if (clientResult != null)
            {
                // User logged in
                ViewBag.status = "Success";
                ViewBag.infor = clientResult;
                HttpContext.Session.SetString("UserSession", JsonSerializer.Serialize(clientResult));
                return Redirect("/Home/Index");
            }

            // If no login was successful
            ViewBag.status = "Fail";
            return Redirect("/Home/Index");
        }

    }
}
