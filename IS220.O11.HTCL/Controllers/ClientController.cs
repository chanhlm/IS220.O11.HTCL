using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using IS220.O11.HTCL.Models;
using System.Web;
using Microsoft.AspNetCore.Http.Extensions;

namespace IS220.O11.HTCL.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult khuyenmai()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            var res = HttpContext.Session.GetString("UserSession");
            if (res != null)
            {
                account usersession = JsonSerializer.Deserialize<account>(res);
                usersession = context.Login(usersession.Tentk, usersession.Matkhau);
                ViewBag.infor = usersession;
                ViewBag.status = "Success";
                string tentk = ViewBag.infor.Tentk;
                ViewBag.ds_voucher = context.User_Voucher(tentk);
                int Matk = Convert.ToInt32(usersession.Matk);
                int sl = 0;
                sl = context.User_Vouchers(Matk);
                ViewBag.soluong = sl;
            }
            else
            {
                ViewBag.Status = "Failed";
            }
            ViewBag.khuyenmai = context.GetVoucher();
            ViewBag.avatar = HttpContext.Session.GetString("Avatar");
            return View();
        }

        /*public IActionResult chitietsach()
        {
            return View();
        }*/

        public IActionResult chitietsach(int name)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            ViewBag.status = "Fail";
            var res = HttpContext.Session.GetString("UserSession");
            if (res != null)
            {
                account usersession = JsonSerializer.Deserialize<account>(res);
                usersession = context.Login(usersession.Tentk, usersession.Matkhau);
                ViewBag.infor = usersession;
                ViewBag.status = "Success";
                ViewBag.avatar = HttpContext.Session.GetString("Avatar");
            }

            //StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            Book book = context.ViewBook(name);
            List<Book> booklist = context.DsLienQuan(name);
            List<comment> listcomments = context.binhluan(book.Masach);
            ViewBag.comments = listcomments;
            ViewBag.book = book;
            ViewBag.books = booklist;
            return View();
        }


        [HttpPost]
        public IActionResult DangKy(account kh)
        {
            int count;

            StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            count = context.DangKy(kh);
            account res = context.Login(kh.Tentk, kh.Matkhau);
            if (res != null)
            {
                ViewBag.status = "Success";
                ViewBag.infor = res;
                HttpContext.Session.SetString("UserSession", JsonSerializer.Serialize(res));
            }
            else
            {
                ViewBag.status = "Fail";
            }
            return Redirect("/Home/Index");
        }

        public IActionResult cart()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            var res = HttpContext.Session.GetString("UserSession");
            if (res != null)
            {
                account usersession = JsonSerializer.Deserialize<account>(res);
                usersession = context.Login(usersession.Tentk, usersession.Matkhau);
                ViewBag.infor = usersession;
                ViewBag.status = "Success";
                List<object> list = context.Cart(usersession.Matk);
                ViewBag.ListCart = list;
                ViewBag.Tentk = usersession.Tentk;
                ViewBag.Hoten = usersession.Hoten;
                ViewBag.Diachi = usersession.Diachi;
                ViewBag.Sodt = usersession.Sodt;
                ViewBag.avatar = HttpContext.Session.GetString("Avatar");
                return View();
            } else return Redirect("/Home/Index");
        }


        public IActionResult themgiohang(string matk, string masach, string soluong)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            context.themvaogiohang(matk, masach, soluong);
            return Redirect("/Home/Index");
        }

        public IActionResult Login()
        {
            string username = HttpContext.Request.Form["username"];
            string password = HttpContext.Request.Form["password"];
            var url = HttpContext.Request.GetEncodedUrl();
            StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            account res = context.Login(username, password);
            if (res != null)
            {
                ViewBag.status = "Success";
                ViewBag.infor = res;
                HttpContext.Session.SetString("UserSession", JsonSerializer.Serialize(res));
            }
            else
            {
                ViewBag.status = "Fail";
            }
            return Redirect("/Home/Index");
        }

        public IActionResult Logout()
        {
            //StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            HttpContext.Session.Remove("UserSession");
            return Redirect("/Home/Index");
        }



        public IActionResult taikhoan(string tentk)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            var res = HttpContext.Session.GetString("UserSession");
            if (res != null)
            {
                account usersession = JsonSerializer.Deserialize<account>(res);
                usersession = context.Login(usersession.Tentk, usersession.Matkhau);
                ViewBag.infor = usersession;
                ViewBag.status = "Success";
            }
            ViewBag.taikhoan = context.Client_Accounts(tentk);
            ViewBag.khuyenmai = context.User_Voucher(tentk);
            ViewBag.avatar = HttpContext.Session.GetString("Avatar");
            int matk = Convert.ToInt32(ViewBag.taikhoan.Matk);
            ViewBag.orders = context.DonHang(matk);
            ViewBag.books = context.BookOfOrder(matk);
            return View();
        }

        public IActionResult capnhattaikhoan(string Matk, string Email, string Sodt, string Gioitinh, string Ngaysinh)
        {
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            count = context.capnhattaikhoan(Matk, Email, Sodt, Gioitinh, Ngaysinh);
            if (count > 0)
            {
                return Redirect("/Client/taikhoan?tentk=" + Matk);
            }
            else
            {
                return Redirect("/Home/Index");
            }

        }

        public IActionResult capnhatdiachi(string Matk, string Sodt, string Diachi, string Hoten)
        {
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            count = context.capnhatdiachi(Matk, Sodt, Diachi, Hoten);
            if (count > 0)
            {
                return Redirect("/Client/taikhoan?tentk=" + Matk);
            }
            else
            {
                return Redirect("/Home/Index");
            }

        }

        public IActionResult capnhatmatkhau(string Matk, string Matkhau)
        {
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            count = context.capnhatmatkhau(Matk, Matkhau);
            if (count > 0)
            {
                return Redirect("/Client/taikhoan?tentk=" + Matk);
            }
            else
            {
                return Redirect("/Home/Index");
            }

        }
        public ActionResult Search_Book(string ten_sach)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            var res = HttpContext.Session.GetString("UserSession");
            if (res != null)
            {
                account usersession = JsonSerializer.Deserialize<account>(res);
                usersession = context.Login(usersession.Tentk, usersession.Matkhau);
                ViewBag.infor = usersession;
                ViewBag.status = "Success";
                ViewBag.avatar = HttpContext.Session.GetString("Avatar");
            }
            List<Book> books = new List<Book>();
            books = context.Search_Book(ten_sach);
            return View(books);
        }

        public ActionResult Search_Category(string cate)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            var res = HttpContext.Session.GetString("UserSession");
            if (res != null)
            {
                account usersession = JsonSerializer.Deserialize<account>(res);
                usersession = context.Login(usersession.Tentk, usersession.Matkhau);
                ViewBag.infor = usersession;
                ViewBag.status = "Success";
                ViewBag.avatar = HttpContext.Session.GetString("Avatar");
            }
            List<Book> books = new List<Book>();
            books = context.Search_Category(cate);
            return View(books);
        }

        public ActionResult Search_Filter(string giaban, string ngonngu, string nhaxuatban)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            List<Book> books = new List<Book>();
            books = context.Search_Filter(giaban, ngonngu, nhaxuatban);

            return View(books);
        }
        
        public ActionResult thembinhluan(comment c)
        {
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            count = context.thembinhluan(c);
            int name = Convert.ToInt32(c.Masach);
            if (count > 0) return Redirect("/Client/chitietsach?name=" + c.Masach);
            return Redirect("/Client/chitietsach?name=" + c.Masach);
        }

        public ActionResult chitietdonhang(int madh)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            var res = HttpContext.Session.GetString("UserSession");
            if (res != null)
            {
                account usersession = JsonSerializer.Deserialize<account>(res);
                usersession = context.Login(usersession.Tentk, usersession.Matkhau);
                ViewBag.infor = usersession;
                ViewBag.status = "Success";
                ViewBag.chitietdh = context.chitietdh(madh);
                int Matk = Convert.ToInt32(usersession.Matk);
                ViewBag.tamtinh = context.Tamtinh(madh);
                ViewBag.orders = context.ViewDonHang(Matk);
                ViewBag.slmua = context.SoluongMua(madh);
                ViewBag.avatar = HttpContext.Session.GetString("Avatar");
                int giamgia = ViewBag.tamtinh + ViewBag.orders.Tienship - ViewBag.orders.Tongtien;
                ViewBag.giamgia = giamgia;

            }
            return View();
        }

        public IActionResult Luukhuyenmai( int Makm)
        {
            int count;

            StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            var res = HttpContext.Session.GetString("UserSession");
            if (res != null)
            {
                account usersession = JsonSerializer.Deserialize<account>(res);
                usersession = context.Login(usersession.Tentk, usersession.Matkhau);
                ViewBag.infor = usersession;
                ViewBag.status = "Success";
                ViewBag.avatar = HttpContext.Session.GetString("Avatar");
                int Matk = Convert.ToInt32(usersession.Matk);
                count = context.Save_voucher( Matk, Makm);
            }
            return Redirect("/Client/khuyenmai");
        }


        /*public IActionResult updategiohang(string matk, string masach, string soluong)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            context.updategiohang(matk, masach, soluong);
            return View();
        }*/

        public class sach
        {
            public string masach { get; set; }
            public string tensach { get; set; }
            public string giaban { get; set; }
            public string soluong { get; set; }
            public string tongtien { get; set; }
            public string hinhanh { get; set; }
            public string theloai { get; set; }
        }

        public IActionResult payment()
        {
            string data = HttpContext.Request.Form["data"];
                StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
                var res = HttpContext.Session.GetString("UserSession");
                if (res != null)
                {
                    account usersession = JsonSerializer.Deserialize<account>(res);
                    usersession = context.Login(usersession.Tentk, usersession.Matkhau);
                    ViewBag.infor = usersession;
                    ViewBag.status = "Success";
                    var temp = JsonSerializer.Deserialize<sach[]>(data);
                    int soluong = 0, thanhtien = 0;
                    foreach (var item in temp)
                    {
                        soluong += Convert.ToInt32(item.soluong);
                        thanhtien += Convert.ToInt32(item.giaban) * Convert.ToInt32(item.soluong);
                    }
                    List<object> ListVoucher = context.get_voucher(usersession.Matk.ToString());
                    ViewBag.ListVoucher = ListVoucher;
                    DateTime dateTime = DateTime.Today;
                    string date = dateTime.ToString("dd/MM/yyyy");
                    ViewBag.Ngaygiao = date;
                    ViewBag.Soluong = soluong;
                    ViewBag.Thanhtien = thanhtien;
                    ViewBag.ListBuyed = temp;
                    ViewBag.Hoten = usersession.Hoten;
                    ViewBag.Diachi = usersession.Diachi;
                    ViewBag.Sodt = usersession.Sodt;
                    ViewBag.Tentk = usersession.Tentk;
                    ViewBag.avatar = HttpContext.Session.GetString("Avatar");
            }
            return View();
        }

        

        public IActionResult thanhyou(string data, string tongtien, string soluong, string hinhthucthanhtoan, string tinhtrangthanhtoan, string tinhtrangdonhang, string tienship, string voucher_used)
        {
            /*var data = HttpContext.Request.Form["data"];
            string tongtien = HttpContext.Request.Form["tongtien"];
            string soluong = HttpContext.Request.Form["soluong"];
            string hinhthucthanhtoan = HttpContext.Request.Form["hinhthucthanhtoan"];
            string tinhtrangthanhtoan = HttpContext.Request.Form["tinhtrangdonhang"];
            string tinhtrangdonhang = HttpContext.Request.Form["tinhtrangdonhang"];*/

            var res = HttpContext.Session.GetString("UserSession");
            if (res != null)
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
                account usersession = JsonSerializer.Deserialize<account>(res);
                usersession = context.Login(usersession.Tentk, usersession.Matkhau);
                ViewBag.infor = usersession;
                ViewBag.status = "Success";
                string matk = usersession.Matk;
               ViewBag.madh = context.thanhyou(matk, data, tongtien, soluong, hinhthucthanhtoan, tinhtrangthanhtoan, tinhtrangdonhang, tienship,voucher_used);
                //context.thanhyou(matk, data, tongtien, soluong, hinhthucthanhtoan, tinhtrangthanhtoan, tinhtrangdonhang, tienship);
                ViewBag.avatar = HttpContext.Session.GetString("Avatar");
            }
            
            return View();
        }

        public IActionResult deletevoucher(string data)
        {
            var res = HttpContext.Session.GetString("UserSession");
            if (res != null)
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
                account usersession = JsonSerializer.Deserialize<account>(res);
                usersession = context.Login(usersession.Tentk, usersession.Matkhau);
                ViewBag.infor = usersession;
                ViewBag.status = "Success";
                ViewBag.avatar = HttpContext.Session.GetString("Avatar");
                context.deletevoucher(usersession.Matk,data);
            }
            return View();
        }

        public IActionResult xoagiohang(string masach)
        {
            var res = HttpContext.Session.GetString("UserSession");
            if (res != null)
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
                account usersession = JsonSerializer.Deserialize<account>(res);
                usersession = context.Login(usersession.Tentk, usersession.Matkhau);
                ViewBag.infor = usersession;
                ViewBag.status = "Success";
                context.xoagiohang(usersession.Matk, masach);
            }
            return View();
        }

       /* public IActionResult themgiohang(string matk, string masach, string soluong)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Models.StoreContext)) as StoreContext;
            context.themvaogiohang(matk, masach, soluong);
            return Redirect("/Home/Index");
        }*/
    }
}
