using IS220.O11.HTCL.Models;
using IS220.O11.HTCL.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace IS220.O11.HTCL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class HomeController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Index(int page)
        {
            StoreContextAdmin context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Areas.Admin.Models.StoreContextAdmin)) as StoreContextAdmin;
            if (page == -1)
            {
                //ViewBag.flashsale = context.FlashSales();
                ViewBag.listbook = context.GetBook(0);
                ViewBag.page = 1;
            }
            else
            {
                //ViewBag.flashsale = context.FlashSales();
                ViewBag.listbook = context.GetBook(page);
                ViewBag.page = page;
            }
            var res = HttpContext.Session.GetString("UserSession");
            if (res != null)
            {
                account usersession = JsonSerializer.Deserialize<account>(res);
                usersession = context.login(usersession.Email, usersession.Matkhau);
                ViewBag.infor = usersession;
                ViewBag.status = "Success";
                ViewBag.avatar = HttpContext.Session.GetString("Avatar");
            }
            return View();
        }
    }
}
