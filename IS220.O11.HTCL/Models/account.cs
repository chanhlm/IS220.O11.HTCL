using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS220.O11.HTCL.Models
{
    public class account
    {
        private string diachi, email, giohang, gioitinh, hoten, matkhau,  sodt, tinhtrang, phanquyen;
        private DateTime ngaysinh, ngaylap;
        private int diem, sl_giohang;
        private int matk;

        public string Diachi { get => diachi; set => diachi = value; }
        public int Diem { get => diem; set => diem = value; }
        public string Email { get => email; set => email = value; }
        public string Giohang { get => giohang; set => giohang = value; }
        public string Gioitinh { get => gioitinh; set => gioitinh = value; }
        public string Hoten { get => hoten; set => hoten = value; }
        public int Matk { get => matk; set => matk = value; }
        public string Matkhau { get => matkhau; set => matkhau = value; }
        public DateTime Ngaysinh { get => ngaysinh; set => ngaysinh = value; }
        public DateTime Ngaylap { get => ngaylap; set => ngaylap = value; }
        public int Sl_giohang { get => sl_giohang; set => sl_giohang = value; }
        public string Sodt { get => sodt; set => sodt = value; }
        public string Tinhtrang { get => tinhtrang; set => tinhtrang = value; }
        public string Phanquyen { get => phanquyen; set => phanquyen = value; }



        public account() {  }
        public account( string diachi, int diem, string email, string giohang, string gioitinh, string hoten, int matk, string matkhau, DateTime ngaysinh, DateTime ngaytao, int sl_giohang, string sodt, string tinhtrang, string phanquyen)
        {
            this.diachi = diachi;
            this.diem = diem;
            this.email = email;
            this.giohang = giohang;
            this.gioitinh = gioitinh;
            this.hoten = hoten;
            this.matk = matk;
            this.matkhau = matkhau;
            this.ngaysinh = ngaysinh;
            this.ngaylap = ngaytao;
            this.sl_giohang = sl_giohang;
            this.sodt = sodt;
            this.tinhtrang = tinhtrang;
            this.Phanquyen = phanquyen;
        }
    }
}
