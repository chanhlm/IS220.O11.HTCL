using MySql.Data.MySqlClient;
using IS220.O11.HTCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS220.O11.HTCL.Areas.Admin.Models
{
    public class StoreContextAdmin
    {
        public string ConnectionString { get; set; }//biết thành viên
        public StoreContextAdmin(string connectionString) //phuong thuc khoi tao
        {
            this.ConnectionString = connectionString;
        }
        private MySqlConnection GetConnection() //lấy connection
        {
            return new MySqlConnection(ConnectionString);
        }

        // Quản lý sách

      

        public List<object> GetBook()
        {
            List<object> list = new List<object>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select s.masach,soluongban,danhgia,giaban,giagoc,giamgia,hinhanh,hinhthuc,mota,ngonngu,nxb,sobinhchon,tacgia,namxb,tensach, theloai, s.soluong from Booklist s";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ob = new
                        {
                            Masach = Convert.ToInt32(reader["masach"]),
                            Danhgia = Convert.ToInt32(reader["danhgia"]),
                            Giaban = Convert.ToInt32(reader["giaban"]),
                            Giagoc = Convert.ToInt32(reader["giagoc"]),
                            Giamgia = Convert.ToInt32(reader["giamgia"]),

                            Hinhanh = reader["hinhanh"].ToString(),
                            Hinhthuc = reader["hinhthuc"].ToString(),
                            Mota = reader["mota"].ToString(),
                            Ngonngu = reader["ngonngu"].ToString(),
                            Nxb = reader["nxb"].ToString(),
                            Sobinhchon = reader["sobinhchon"].ToString(),
                            Tacgia = reader["tacgia"].ToString(),
                            Namxb = Convert.ToDateTime(reader["namxb"]),

                            Tensach = reader["tensach"].ToString(),
                            Theloai = reader["theloai"].ToString(),
                            Soluong = Convert.ToInt32(reader["soluong"]),
                            Soluongban = Convert.ToInt32(reader["soluongban"]),

                        };
                        list.Add(ob);
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

        public int InsertBook(Book bk)
        {
            using(MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "INSERT INTO Booklist values(@masach, @danhgia, @giaban, @giagoc, @giamgia, @hinhanh, @hinhthuc, @mota, @namxb,@ngonngu, @nxb, @sobinhchon, @tacgia, @tensach, @theloai, @soluong )";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("masach", bk.Masach);
                cmd.Parameters.AddWithValue("danhgia", bk.Danhgia);
                cmd.Parameters.AddWithValue("giaban", bk.Giaban);
                cmd.Parameters.AddWithValue("giagoc", bk.Giagoc);
                cmd.Parameters.AddWithValue("giamgia", bk.Giamgia);
                cmd.Parameters.AddWithValue("hinhanh", bk.Hinhanh);
                cmd.Parameters.AddWithValue("hinhthuc", bk.Hinhthuc);
                cmd.Parameters.AddWithValue("mota", bk.Mota);
                cmd.Parameters.AddWithValue("namxb", bk.Namxb);
                cmd.Parameters.AddWithValue("ngonngu", bk.Ngonngu);
                cmd.Parameters.AddWithValue("nxb", bk.Nxb);
                cmd.Parameters.AddWithValue("sobinhchon", bk.Sobinhchon);
                cmd.Parameters.AddWithValue("tacgia", bk.Tacgia);
                cmd.Parameters.AddWithValue("tensach", bk.Tensach);
                cmd.Parameters.AddWithValue("theloai", bk.Theloai);
                cmd.Parameters.AddWithValue("soluong", bk.Soluong);
                return cmd.ExecuteNonQuery();
            }
        }

        public int XoaSach(int Id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "delete from Booklist where masach=@Masach";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("Masach", Id);
                return (cmd.ExecuteNonQuery());
            }
        }

        public Book GetBookById(int Id)
        {

            Book list = new Book();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from Booklist where masach=@Masach";
                
                MySqlCommand d = new MySqlCommand(str, conn);
                d.Parameters.AddWithValue("Masach", Id);
                

                using (var reader = d.ExecuteReader())
                {
                    reader.Read();
                    
                    list.Danhgia = Convert.ToInt32(reader["danhgia"]);
                    list.Giaban = Convert.ToInt32(reader["giaban"]);
                    list.Giagoc = Convert.ToInt32(reader["giagoc"]);
                    list.Giamgia = Convert.ToInt32(reader["giamgia"]);
                    list.Hinhanh = reader["hinhanh"].ToString();
                    list.Hinhthuc = reader["hinhthuc"].ToString();                        
                    list.Mota = reader["mota"].ToString();
                    list.Nxb = reader["nxb"].ToString();
                    list.Sobinhchon = Convert.ToInt32(reader["sobinhchon"]);
                    list.Tacgia = reader["tacgia"].ToString();
                    list.Namxb = Convert.ToDateTime(reader["namxb"]);
                    list.Tensach = reader["tensach"].ToString();
                    list.Theloai = reader["theloai"].ToString();
                    list.Soluong = Convert.ToInt32(reader["soluong"]);
                    list.Masach = Convert.ToInt32(reader["masach"]);

                };
            }
            return list;
        }

        public int GetSLBan(int Id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                int sumsoluong = 0;
                conn.Open();
                string str = "select sum(soluong) as sl from detail_order where masach=@Masach";

                MySqlCommand d = new MySqlCommand(str, conn);
                d.Parameters.AddWithValue("Masach", Id);

                using (var reader = d.ExecuteReader())
                {
                    reader.Read();
                    sumsoluong = Convert.ToInt32(reader["sl"]);

                };
                return sumsoluong;
            }
        }

        public int UpdateBookById(Book kh)
        {
            Console.WriteLine(kh.Masach);
            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                var str = "UPDATE  Booklist SET danhgia=@danhgia,giaban=@giaban,giagoc=@giagoc,giamgia=@giamgia," +
                    "hinhanh=@hinhanh,hinhthuc=@hinhthuc,mota=@mota,namxb=@namxb,ngonngu=@ngonngu,nxb=@nxb," +
                    "sobinhchon=@sobinhchon,tacgia=@tacgia,tensach=@tensach, soluong=@soluong, theloai=@theloai WHERE masach=@masach";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("masach", kh.Masach);
                cmd.Parameters.AddWithValue("danhgia", kh.Danhgia);
                cmd.Parameters.AddWithValue("giaban", kh.Giaban);
                cmd.Parameters.AddWithValue("giagoc", kh.Giagoc);
                cmd.Parameters.AddWithValue("giamgia", kh.Giamgia);
                cmd.Parameters.AddWithValue("hinhanh", kh.Hinhanh);
                cmd.Parameters.AddWithValue("hinhthuc", kh.Hinhthuc);
                cmd.Parameters.AddWithValue("mota", kh.Mota);
                cmd.Parameters.AddWithValue("namxb", kh.Namxb);
                cmd.Parameters.AddWithValue("ngonngu", kh.Ngonngu);
                cmd.Parameters.AddWithValue("nxb", kh.Nxb);
                cmd.Parameters.AddWithValue("sobinhchon", kh.Sobinhchon);
                cmd.Parameters.AddWithValue("tacgia", kh.Tacgia);
                cmd.Parameters.AddWithValue("tensach", kh.Tensach);
                cmd.Parameters.AddWithValue("theloai", kh.Theloai);
                cmd.Parameters.AddWithValue("soluong", kh.Soluong);


                return (cmd.ExecuteNonQuery());
            }
        }


        // Quản lý tài khoản]
        public List<account> GetAccount()
        {
            List<account> list = new List<account>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from accounts";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new account()
                        {
                            Diachi = reader["diachi"].ToString(),
                            Matk = Convert.ToInt32(reader["matk"]),
                            Diem = Convert.ToInt32(reader["diem"]),
                            Sl_giohang = Convert.ToInt32(reader["sl_giohang"]),
                            Email = reader["email"].ToString(),
                            Gioitinh = reader["gioitinh"].ToString(),
                            Hoten = reader["hoten"].ToString(),
                            Matkhau = reader["matkhau"].ToString(),
                            Ngaytao = Convert.ToDateTime(reader["ngaytao"]),
                            Tinhtrang = reader["tinhtrang"].ToString(),
                            Sodt = reader["sodt"].ToString(),
                            Ngaysinh = Convert.ToDateTime(reader["ngaysinh"]),


                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

        public List<account> GetAccountById(int Id)
        {
            List<account> list = new List<account>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from accounts where matk=@matk";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("matk", Id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new account()
                        {
                            Diachi = reader["diachi"].ToString(),
                            Matk = Convert.ToInt32(reader["matk"]),
                            Diem = Convert.ToInt32(reader["diem"]),
                            Sl_giohang = Convert.ToInt32(reader["sl_giohang"]),
                            Email = reader["email"].ToString(),
                            Gioitinh = reader["gioitinh"].ToString(),
                            Hoten = reader["hoten"].ToString(),
                            Matkhau = reader["matkhau"].ToString(),
                            Ngaytao = Convert.ToDateTime(reader["ngaytao"]),
                            Tinhtrang = reader["tinhtrang"].ToString(),
                            Sodt = reader["sodt"].ToString(),
                            Ngaysinh = Convert.ToDateTime(reader["ngaysinh"]),


                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

        public int KhoaTK(account kh)
        {

            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                var str = "UPDATE accounts SET tinhtrang='Đã khóa' WHERE matk=@matk";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                Console.WriteLine(kh.Matk);
                Console.WriteLine(kh.Tinhtrang);
                cmd.Parameters.AddWithValue("matk", kh.Matk);
                cmd.Parameters.AddWithValue("Đã khóa", kh.Tinhtrang);
                return (cmd.ExecuteNonQuery());
            }
        }

        public int MoTK(account kh)
        {

            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                var str = "UPDATE accounts SET tinhtrang='Đang sử dụng' WHERE matk=@matk";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                Console.WriteLine(kh.Matk);
                Console.WriteLine(kh.Tinhtrang);
                cmd.Parameters.AddWithValue("matk", kh.Matk);
                cmd.Parameters.AddWithValue("Đang sử dụng", kh.Tinhtrang);
                return (cmd.ExecuteNonQuery());
            }
        }


        public List<user_voucher> GetUserVoucher()
        {
            List<user_voucher> list = new List<user_voucher>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from user_voucher";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new user_voucher()
                        {
                           
                            Makm = Convert.ToInt32(reader["makm"]),
                            Matk = Convert.ToInt32(reader["matk"]),

                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }


        //Quản lý khuyến mãi
        public List<object> GetKhuyenMai()
        {
            List<object> list = new List<object>();
            
            using (MySqlConnection conn = GetConnection())
            {
                DateTime now = DateTime.Now;

                conn.Open();
                string str = @"select img, daluu, dieukien, makm, phantram, sl, loai, manhap, noidung, ngaybd, 
                    ngaykt,day(ngaybd) as ngaybatdau, month(ngaybd) as thangbatdau, year(ngaybd) as nambatdau,
                    day(ngaykt) as ngayketthuc, month(ngaykt) as thangketthuc, year(ngaykt) as namketthuc 
                    from voucher";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        var ngaybatdau = Convert.ToInt32(reader["ngaybatdau"]);
                        var thangbatdau = Convert.ToInt32(reader["thangbatdau"]);
                        var nambatdau = Convert.ToInt32(reader["nambatdau"]);
                        var ngayketthuc = Convert.ToInt32(reader["ngayketthuc"]);
                        var thangketthuc = Convert.ToInt32(reader["thangketthuc"]);
                           var namketthuc = Convert.ToInt32(reader["namketthuc"]);
                        var tinhTrang = "";
                        if (nambatdau <= now.Year)
                        {
                            if (ngaybatdau > now.Day && thangbatdau > now.Month)
                                tinhTrang = "Chưa bắt đầu";
                            else
                            {
                                if (thangbatdau == now.Month)
                                {
                                    if (ngaybatdau > now.Day)
                                    {
                                        tinhTrang = "Chưa bắt đầu";
                                    }
                                    else
                                    {
                                        if (namketthuc == now.Year)
                                        {
                                            if (ngayketthuc >= now.Day && thangketthuc >= now.Month)
                                                tinhTrang = "Đang diễn ra";
                                            else
                                                tinhTrang = "Đã kết thúc";
                                        }
                                        else
                                        {
                                            if (namketthuc > now.Year)
                                                tinhTrang = "Đang diễn ra";
                                            else
                                                tinhTrang = "Đã kết thúc";
                                        }
                                    }
                                }
                                else
                                {
                                    if (namketthuc == now.Year)
                                    {
                                        if (thangketthuc > now.Month)
                                            tinhTrang = "Đang diễn ra";
                                        else
                                            if(thangketthuc == now.Month)
                                            {
                                                if(ngayketthuc<now.Day)
                                                    tinhTrang = "Đã kết thúc";
                                                else
                                                tinhTrang = "Đang diễn ra";
                                        }
                                            else
                                                tinhTrang = "Đã kết thúc";
                                    }
                                    else
                                    {
                                        if (namketthuc > now.Year)
                                            tinhTrang = "Đang diễn ra";
                                        else
                                            tinhTrang = "Đã kết thúc";
                                    }
                                }
                            }   
                        }
                        else
                        {
                            tinhTrang = "Chưa bắt đầu";
                        }
                        var ob = new
                        {
                            Img = reader["img"].ToString(),
                            Daluu = Convert.ToInt32(reader["daluu"]),
                            Dieukien = Convert.ToInt32(reader["dieukien"]),
                            Makm = Convert.ToInt32(reader["makm"]),
                            Phantram = Convert.ToInt32(reader["phantram"]),
                            Sl = Convert.ToInt32(reader["sl"]),

                            Loai = reader["loai"].ToString(),
                            Manhap = reader["manhap"].ToString(),
                            Noidung = reader["noidung"].ToString(),
                            Ngaybd=Convert.ToDateTime(reader["ngaybd"]),
                            Ngaykt = Convert.ToDateTime(reader["ngaykt"]),
                            TinhTrang = tinhTrang,
                             

                    }; list.Add(ob);

                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

 /*       public string TinhTrangKhuyenMai()
        {

            using (MySqlConnection conn = GetConnection())
            {
                string tinhTrang = "";
                
                int ngaybatdau, ngayketthuc, thangketthuc, namketthuc, nambatdau, thangbatdau  = 0 ;
                string str = @"select day(ngaybd) as ngaybatdau, month(ngaybd) as thangbatdau, year(ngaybd) as nambatdau,
                    day(ngaykt) as ngayketthuc, month(ngaykt) as thangketthuc, year(ngaykt) as namketthuc from voucher";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                DateTime now = DateTime.Now;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reader.Read();

                        ngaybatdau = Convert.ToInt32(reader["ngaybatdau"]);
                        thangbatdau = Convert.ToInt32(reader["thangbatdau"]);
                        nambatdau = Convert.ToInt32(reader["nambatdau"]);
                        ngayketthuc = Convert.ToInt32(reader["ngayketthuc"]);
                        thangketthuc = Convert.ToInt32(reader["thangketthuc"]);
                        namketthuc = Convert.ToInt32(reader["namketthuc"]);
                        if (nambatdau < now.Year)
                        {
                            if (namketthuc >= now.Year)
                            {
                                if (thangketthuc >= now.Month)
                                {
                                    if (ngayketthuc >= now.Day)
                                    {
                                        tinhTrang = "Đang diễn ra";
                                    }
                                    if (ngayketthuc < now.Day)
                                    {
                                        tinhTrang = "Đã kết thúc";
                                    }                                  
                                }
                            }
                            else
                            {
                                tinhTrang = "Đã kết thúc";
                            }
                            
                        }
                        if (nambatdau == now.Year)
                        {
                            if (thangbatdau == now.Month)
                            {
                                if (ngaybatdau == now.Day|| (ngaybatdau < now.Day && ngayketthuc >= now.Day))
                                {
                                    tinhTrang = "Đang diễn ra";
                                }
                                if (ngaybatdau < now.Day)
                                {
                                    tinhTrang = "Chưa bắt đầu";
                                }
                                if(ngayketthuc < now.Day)
                                {
                                    tinhTrang = "Đã kết thúc";
                                }
                            }
                            if (thangbatdau < now.Month)
                            {
                                if (thangketthuc > now.Month)
                                {
                                    tinhTrang = "Đang diễn ra";
                                }
                                if(thangketthuc== now.Month)
                                {
                                    if(ngayketthuc>= now.Day)
                                    {
                                        tinhTrang = "Đang diễn ra"; 
                                    }
                                    if(ngayketthuc< now.Day)
                                        tinhTrang = "Đã kết thúc";
                                }
                                else
                                    tinhTrang = "Đã kết thúc";
                            }
                            if(thangbatdau > now.Month)
                                tinhTrang = "Chưa bắt đầu";
                        }
                        if(nambatdau > now.Year)
                        {
                            tinhTrang = "Chưa bắt đầu";
                        }    

                    }
                }
                return tinhTrang;
            }

        }*/

        public voucher GetKhuyenMaiById(int id)
        {
            voucher list = new voucher();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from voucher where makm=@makm";

                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("makm", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        list.Daluu = Convert.ToInt32(reader["daluu"]);
                        list.Dieukien = Convert.ToInt32(reader["dieukien"]);
                        list.Makm = Convert.ToInt32(reader["makm"]);
                        list.Phantram = Convert.ToInt32(reader["phantram"]);
                        list.Sl = Convert.ToInt32(reader["sl"]);
                        list.Loai = reader["loai"].ToString();
                        list.Manhap = reader["manhap"].ToString();
                        list.Noidung = reader["noidung"].ToString();
                        list.Img = reader["img"].ToString();

                        list.Ngaybd = Convert.ToDateTime(reader["ngaybd"]);
                        list.Ngaykt = Convert.ToDateTime(reader["ngaykt"]);
                    }
                    
                    };
                return list;

            }
        }

        public int UpdateKhuyenMaiById(voucher kh)
        {
            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                var str = "UPDATE  voucher SET daluu=@daluu,dieukien=@dieukien,img=@img,loai=@loai," +
                    "manhap=@manhap,ngaybd=@ngaybd,ngaykt=@ngaykt,noidung=@noidung,phantram=@phantram,sl=@sl WHERE makm=@makm";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("daluu", kh.Daluu);
                cmd.Parameters.AddWithValue("dieukien", kh.Dieukien);
                cmd.Parameters.AddWithValue("img", kh.Img);
                cmd.Parameters.AddWithValue("loai", kh.Loai);
                cmd.Parameters.AddWithValue("manhap", kh.Manhap);
                cmd.Parameters.AddWithValue("ngaybd", kh.Ngaybd);
                cmd.Parameters.AddWithValue("ngaykt", kh.Ngaykt);
                cmd.Parameters.AddWithValue("phantram", kh.Phantram);
                cmd.Parameters.AddWithValue("sl", kh.Sl);
                cmd.Parameters.AddWithValue("noidung", kh.Noidung);
                cmd.Parameters.AddWithValue("makm", kh.Makm);
                return (cmd.ExecuteNonQuery());
            }
        }

        public int XoaKhuyenMai(int Id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "delete from voucher where makm=@Makm";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("Makm", Id);
                return (cmd.ExecuteNonQuery());
            }
        }

        public int InsertKhuyenmai(voucher bk)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "INSERT INTO voucher values(@daluu, @dieukien, @img, @loai, @makm, @manhap, @ngaybd, @ngaykt, @noidung,@phantram, @sl )";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("daluu", bk.Daluu);
                cmd.Parameters.AddWithValue("dieukien", bk.Dieukien);
                cmd.Parameters.AddWithValue("img", bk.Img);
                cmd.Parameters.AddWithValue("loai", bk.Loai);
                cmd.Parameters.AddWithValue("makm", bk.Makm);
                cmd.Parameters.AddWithValue("manhap", bk.Manhap);
                cmd.Parameters.AddWithValue("ngaybd", bk.Ngaybd);
                cmd.Parameters.AddWithValue("ngaykt", bk.Ngaykt);
                cmd.Parameters.AddWithValue("noidung", bk.Noidung);
                cmd.Parameters.AddWithValue("phantram", bk.Phantram);
                cmd.Parameters.AddWithValue("sl", bk.Sl);
                return cmd.ExecuteNonQuery();
            }
        }



        //Quản lý đơn hàng'

        public List<order> GetDonHang()
        {
            List<order> list = new List<order>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from order";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new order()
                        {

                            Madh = Convert.ToInt32(reader["madh"]),
                            Matk = Convert.ToInt32(reader["matk"]),
                            Makm = Convert.ToInt32(reader["makm"]),

                            Tienship = Convert.ToInt32(reader["tienship"]),
                            Phanhoi = reader["phanhoi"].ToString(),
                            Tinhtrangdonhang = reader["tinhtrangdonhang"].ToString(),
                            Tinhtrangthanhtoan = reader["tinhtrangthanhtoan"].ToString(),
                            Tongtien = Convert.ToInt32(reader["tongtien"]),
                            Hinhthucthanhtoan = reader["hinhthucthanhtoan"].ToString(),

                               Ngaycapnhat = Convert.ToDateTime(reader["ngaycapnhat"]),
                               Ngaylap = Convert.ToDateTime(reader["ngaylap"]),


                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

         public List<order> FilterDonHang(DateTime Start, DateTime End)
        {
            List<order> list = new List<order>();

            using (MySqlConnection conn = GetConnection())
            {

                

                string start = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Start));
                string end = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(End));

                conn.Open();
                string str = "select * from order where (ngaylap BETWEEN @start AND @end)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("start", start);
                cmd.Parameters.AddWithValue("end", end);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new order()
                        {

                            Madh = Convert.ToInt32(reader["madh"]),
                            Matk = Convert.ToInt32(reader["matk"]),
                            Makm = Convert.ToInt32(reader["makm"]),

                            Tienship = Convert.ToInt32(reader["tienship"]),
                            Phanhoi = reader["phanhoi"].ToString(),
                            Tinhtrangdonhang = reader["tinhtrangdonhang"].ToString(),
                            Tinhtrangthanhtoan = reader["tinhtrangthanhtoan"].ToString(),
                            Tongtien = Convert.ToInt32(reader["tongtien"]),
                            Hinhthucthanhtoan = reader["hinhthucthanhtoan"].ToString(),

                            Ngaycapnhat = Convert.ToDateTime(reader["ngaycapnhat"]),
                            Ngaylap = Convert.ToDateTime(reader["ngaylap"]),


                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

        public order ViewDonHang(int Id)
        {
            order kh = new order();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select * from order where madh=@Id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("Id", Id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        kh = new order()
                        {
                            Madh = Convert.ToInt32(reader["madh"]),
                            Matk = Convert.ToInt32(reader["matk"]),
                            Makm = Convert.ToInt32(reader["makm"]),

                            Tienship = Convert.ToInt32(reader["tienship"]),
                            Phanhoi = reader["phanhoi"].ToString(),

                            Tinhtrangdonhang = reader["tinhtrangdonhang"].ToString(),
                            Tinhtrangthanhtoan = reader["tinhtrangthanhtoan"].ToString(),
                            Tongtien = Convert.ToInt32(reader["tongtien"]),
                            Hinhthucthanhtoan = reader["hinhthucthanhtoan"].ToString(),



                          Ngaycapnhat = Convert.ToDateTime(reader["ngaycapnhat"]),
                           Ngaylap = Convert.ToDateTime(reader["ngaylap"]),
                        }; 
                    } 
                }
            }
            return (kh);
        }

        public List<object> GetObject_Book(int Id)
        {
            List<object> list = new List<object>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select o.masach,tongtien, giaban, o.soluong, o.madh, tienship from Booklist s, detail_order o, order where s.masach=o.masach and order.madh=o.madh and o.madh=@Madh";

                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("Madh", Id);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ob = new
                        {
                            Masach = Convert.ToInt32(reader["masach"]),
                            Giaban = Convert.ToInt32(reader["giaban"]),
                            Soluong = Convert.ToInt32(reader["soluong"]),
                            Tongtien = Convert.ToInt32(reader["tongtien"]),
                            Madh = Convert.ToInt32(reader["madh"]),
                            Tienship = Convert.ToInt32(reader["tienship"]),
                            thanhtien = Convert.ToInt32(reader["soluong"]) * Convert.ToInt32(reader["giaban"]),
                        };
                        list.Add(ob);
                    }
                    reader.Close();
                }

                conn.Close();   

            }
            return list;
        }

        public float GetPhanTramKM(int Id)
        {
            

            using (MySqlConnection conn = GetConnection())
            {
                float tiengiam = 0;
                conn.Open();
                string str = "select phantram, tongtien, sum(o.soluong*giaban) as tamtinh from  Booklist b, detail_order o, order, voucher km where b.masach=o.masach and order.madh=o.madh and order.makm=km.makm and o.madh=@Madh group by o.madh";
                
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("Madh", Id);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reader.Read();
                        tiengiam = (float)((Convert.ToInt32(reader["phantram"]) * Convert.ToInt32(reader["tamtinh"])) / 100);
                    }
                }
                return tiengiam;
            }
            
        }

        public float TinhThanhTien(int Madh)
        {
            using(MySqlConnection conn = GetConnection())
            {
                /*float thanhtien = 0; 
                int Tongtien = 0;
                int Tienship = 0;
                int phantram = 0;*/
                int tamtinh = 0 ;
                conn.Open();
                string str = "SELECT sum(o.soluong*giaban) as tamtinh  from Booklist b, detail_order o, order where b.masach=o.masach and o.madh=order.madh and o.madh=@madh group by o.madh";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("madh", Madh);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reader.Read();
                        /*Tongtien = Convert.ToInt32(reader["tongtien"]);
                        Tienship = Convert.ToInt32(reader["tienship"]);
                        phantram= Convert.ToInt32(reader["phantram"]);
                        thanhtien = (Convert.ToInt32(reader["tongtien"]) - ((float)((Convert.ToInt32(reader["tongtien"]) * Convert.ToInt32(reader["phantram"]))/100)) + Convert.ToInt32(reader["tienship"]));*/
                        tamtinh = Convert.ToInt32(reader["tamtinh"]);
                    }
                    return tamtinh;

                }

            }
        }


        public int UpdateDonHangById(int Id, string Tinhtrangdonhang, string Phanhoi)
        {


           
            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                var str = @"UPDATE  order 
                            SET phanhoi=@phanhoi,tinhtrangdonhang=@tinhtrangdonhang 
                            WHERE madh=@madh";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                
            
                cmd.Parameters.AddWithValue("phanhoi", Phanhoi);
                cmd.Parameters.AddWithValue("tinhtrangdonhang", Tinhtrangdonhang);                
                cmd.Parameters.AddWithValue("madh", Id);
                return (cmd.ExecuteNonQuery());
            }
        }



        //Thống kê

        public List<object> ThongKe()
        {
            List<object> list = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT DISTINCT sum(o.soluong) as slSach, count(o.madh) as slDonHang, count(order.matk) as slTaiKhoan, sum(tongtien) as tongTien  from Booklist b, detail_order o, order where b.masach=o.masach and o.madh=order.madh and tinhtrangthanhtoan = 'Đã thanh toán'";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ob = new
                        {
                            soluongSach = Convert.ToInt32(reader["slSach"]),
                            soluongDonHang = Convert.ToInt32(reader["slDonHang"]),
                            soluongTaiKhoan = Convert.ToInt32(reader["slTaiKhoan"]),
                            tongTien = Convert.ToInt32(reader["tongTien"]),

                            
                        };
                        list.Add(ob);
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

        public List<object> GetThongKeTheLoai()
        {
            List<object> list = new List<object>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT theloai, sum(o.soluong) as slban from Booklist b, detail_order o where b.masach = o.masach GROUP by theloai";

                MySqlCommand cmd = new MySqlCommand(str, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ob = new
                        {
                            Theloai = reader["theloai"].ToString(),
                            slban = Convert.ToInt32(reader["slban"]),               
                        };
                        list.Add(ob);
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

        public List<object> FilterThongKe(DateTime Start, DateTime End)
        {
            List<object> list = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                string start = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Start));
                string end = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(End));
                conn.Open();
                string str = "SELECT DISTINCT sum(o.soluong) as slSach, count(o.madh) as slDonHang, count(order.matk) as slTaiKhoan, sum(tongtien) as tongTien  from Booklist b, detail_order o, order where b.masach=o.masach and o.madh=order.madh and (ngaylap BETWEEN @start AND @end) and tinhtrangthanhtoan = 'Đã thanh toán'";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("start", start);
                cmd.Parameters.AddWithValue("end", end);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ob = new
                        {
                            soluongSach = Convert.ToInt32(reader["slSach"]),
                            soluongDonHang = Convert.ToInt32(reader["slDonHang"]),
                            soluongTaiKhoan = Convert.ToInt32(reader["slTaiKhoan"]),
                            tongTien = Convert.ToInt32(reader["tongTien"]),


                        };
                        list.Add(ob);
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

        public List<object> FilterThongKeTheLoai(DateTime Start, DateTime End)
        {
            List<object> list = new List<object>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string start = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Start));
                string end = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(End));
                string str = @"SELECT theloai, sum(o.soluong) as slban from Booklist b, detail_order o where b.masach = o.masach 
                            and o.masach in (SELECT DISTINCT o.masach
                            from Booklist b, detail_order o, order
                            where b.masach = o.masach and o.madh = order.madh and (ngaylap BETWEEN @start AND @end) and tinhtrangthanhtoan = 'Đã thanh toán') GROUP by theloai";

                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("start", start);
                cmd.Parameters.AddWithValue("end", end);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ob = new
                        {
                            Theloai = reader["theloai"].ToString(),
                            slban = Convert.ToInt32(reader["slban"]),
                        };
                        list.Add(ob);
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

        public List<object> DataDoanhThu()
        {
            List<object> list = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = @"SELECT DISTINCT sum(tongtien) as doanhthu, Month(order.ngaylap) as month
                            from Booklist b, detail_order o, order
                                where b.masach = o.masach and o.madh = order.madh and tinhtrangthanhtoan = 'Đã thanh toán'
                                GROUP by Month(order.ngaylap)
                            ORDER BY Month(order.ngaylap) ASC";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ob = new
                        {
                            doanhthu = Convert.ToInt32(reader["doanhthu"]),
                            month = Convert.ToInt32(reader["month"]),
                        };
                        list.Add(ob);
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

        public List<object> FilterDoanhThu(DateTime Start, DateTime End)
        {
            string start = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Start));
            string end = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(End));
            List<object> list = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = @"SELECT DISTINCT sum(tongtien) as doanhthu, Month(order.ngaylap) as month
                            from Booklist b, detail_order o, order
                                where b.masach = o.masach and o.madh = order.madh and tinhtrangthanhtoan = 'Đã thanh toán'and (ngaylap BETWEEN @start AND @end)
                                GROUP by Month(order.ngaylap)
                            ORDER BY Month(order.ngaylap) ASC";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("start", start);
                cmd.Parameters.AddWithValue("end", end);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ob = new
                        {
                            doanhthu = Convert.ToInt32(reader["doanhthu"]),
                            month = Convert.ToInt32(reader["month"]),
                        };
                        list.Add(ob);
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }


        public account login(string email, string password)
        {
            account admin_Accounts = new account();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select * from account where email=@email and matkhau=@password";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("password", password);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        admin_Accounts.Matk = Convert.ToInt32(reader["matk"]);
                        admin_Accounts.Email = reader["email"].ToString();
                        admin_Accounts.Hoten = reader["hoten"].ToString();
                        admin_Accounts.Matkhau = reader["matkhau"].ToString();
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return admin_Accounts;
        }


    }
}
