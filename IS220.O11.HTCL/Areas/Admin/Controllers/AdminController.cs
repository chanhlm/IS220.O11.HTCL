using Microsoft.AspNetCore.Mvc;
using IS220.O11.HTCL.Areas.Admin.Models;
using IS220.O11.HTCL.Models;
using static System.Reflection.Metadata.BlobBuilder;
using System.Text.Json;


namespace IS220.O11.HTCL.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly StoreContextAdmin _storeContext;

        public AdminController(ILogger<AdminController> logger, StoreContextAdmin storeContext)
        {
            _logger = logger;
            _storeContext = storeContext;
        }

        public IActionResult Index()
        {

            return View();
        }


        public IActionResult CapNhat_Sach(int Id)
        {

            ViewBag.Book = _storeContext.GetBookById(Id);
            // ViewBag.SLBan = _storeContext.GetSLBan(Id); 
            return View();
        }
        public IActionResult ThemSach()
        {
            return View();
        }
        public IActionResult InsertBook(Book bk)
        {
            _storeContext.InsertBook(bk);

            return RedirectToAction("Index", "Home");

        }
        public IActionResult UpdateById(Book book)
        {
            _storeContext.UpdateBookById(book);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteBook(int Id)
        {
            _storeContext.XoaSach(Id);
            return RedirectToAction("Index", "Home");

        }


        public IActionResult QL_taikhoan()
        {

            ViewBag.listaccount = _storeContext.GetAccount();
            ViewBag.uservoucher = _storeContext.GetUserVoucher();
            return View();
        }

        public IActionResult KhoaTaiKhoan(account Id)
        {
            _storeContext.KhoaTK(Id);
            return RedirectToAction("QL_taikhoan", "Admin");
        }

        public IActionResult MoTaiKhoan(account Id)
        {
            _storeContext.MoTK(Id);
            return RedirectToAction("QL_taikhoan", "Admin");
        }

        public IActionResult DS_KhuyenMai()
        {
            ViewBag.khuyenmai = _storeContext.GetKhuyenMai();
            /* ViewBag.TinhTrangKhuyenMai = _storeContext.TinhTrangKhuyenMai();*/
            return View();



        }

        public IActionResult CapNhap_KM(int Id)
        {
            ViewBag.khuyenmai = _storeContext.GetKhuyenMaiById(Id);
            return View();

        }

        public IActionResult UpdateKhuyenMaiById(voucher km)
        {
            _storeContext.UpdateKhuyenMaiById(km);
            return RedirectToAction("DS_KhuyenMai", "Admin");
        }

        public IActionResult Xoa_KM(int Id)
        {
            _storeContext.XoaKhuyenMai(Id);
            return RedirectToAction("DS_KhuyenMai", "Admin");

        }

        public IActionResult Them_KM()
        {
            return View();
        }
        public IActionResult InsertKhuyenmai(voucher bk)
        {
            _storeContext.InsertKhuyenmai(bk);
            return RedirectToAction("DS_KhuyenMai", "Admin");

        }


        // ĐƠN HÀNG

        public IActionResult DS_DonHang()
        {
            ViewBag.listDonHang = _storeContext.GetDonHang();
            ViewBag.listaccount = _storeContext.GetAccount();

            return View();
        }
        public IActionResult FilterDonHang(DateTime start, DateTime end)
        {
            ViewBag.listDonHang = _storeContext.FilterDonHang(start, end);
            ViewBag.listaccount = _storeContext.GetAccount();
            return View("DS_DonHang");

        }
        public IActionResult FilterCNDonHang(DateTime start, DateTime end)
        {
            ViewBag.listDonHang = _storeContext.FilterDonHang(start, end);
            ViewBag.listaccount = _storeContext.GetAccount();
            return View("CapNhat_VanChuyen");

        }
        public IActionResult VanChuyen(int Madh, int Matk)
        {
            ViewBag.listDonHang = _storeContext.ViewDonHang(Madh);
            ViewBag.listaccount = _storeContext.GetAccountById(Matk);
            ViewBag.listbook = _storeContext.GetObject_Book(Madh);
            ViewBag.ThanhTien = _storeContext.TinhThanhTien(Madh);
            ViewBag.TienGiam = _storeContext.GetPhanTramKM(Madh);
            return View();
        }

        public IActionResult CapNhat_VanChuyen()
        {

            ViewBag.listDonHang = _storeContext.GetDonHang();
            ViewBag.listaccount = _storeContext.GetAccount();
            return View();
        }


        public IActionResult UpdateDonHangById(int Id, string Tinhtrangdonhang, string Phanhoi)
        {
            _storeContext.UpdateDonHangById(Id, Tinhtrangdonhang, Phanhoi);
            return RedirectToAction("CapNhat_VanChuyen", "Admin");
        }

        public IActionResult ThongKe()
        {
            ViewBag.ThongKe = _storeContext.ThongKe();
            ViewBag.ThongKeTheLoai = _storeContext.GetThongKeTheLoai();
            ViewBag.DataDoanhThu = _storeContext.DataDoanhThu();

            return View();
        }

        public IActionResult FilterThongKe(DateTime start, DateTime end)
        {
            ViewBag.ThongKe = _storeContext.FilterThongKe(start, end);
            ViewBag.ThongKeTheLoai = _storeContext.FilterThongKeTheLoai(start, end);
            ViewBag.DataDoanhThu = _storeContext.FilterDoanhThu(start, end);
            return View("ThongKe");

        }

        public IActionResult login()
        {
            string email = HttpContext.Request.Form["email"];
            string password = HttpContext.Request.Form["password"];

            StoreContextAdmin context = HttpContext.RequestServices.GetService(typeof(IS220.O11.HTCL.Areas.Admin.Models.StoreContextAdmin)) as StoreContextAdmin;
            account res = context.login(email, password);
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
            return View();
        }
    }
}
