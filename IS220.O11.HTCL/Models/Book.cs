using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS220.O11.HTCL.Models
{
    public class Book
    {
        private int masach, danhgia;
        private string tensach;
        private string hinhanh;
        private string theloai;
        private int giaban;
        private int giagoc;
        private int giamgia;
        private string mota;
        private string ngonngu;
        private string tacgia;
        private DateTime namxb;
        private string nxb;
        private string hinhthuc;
        private int soluong;
        private int sobinhchon;
        private int soluongban;

        public int Masach { get => masach; set => masach = value; }
        public string Tensach { get => tensach; set => tensach = value; }
        public string Hinhanh { get => hinhanh; set => hinhanh = value; }
        public string Theloai { get => theloai; set => theloai = value; }
        public int Giaban { get => giaban; set => giaban = value; }
        public int Giagoc { get => giagoc; set => giagoc = value; }
        public int Giamgia { get => giamgia; set => giamgia = value; }
        public string Mota { get => mota; set => mota = value; }

        public string Ngonngu { get => ngonngu; set => ngonngu = value; }
        public string Tacgia { get => tacgia; set => tacgia = value; }
        public DateTime Namxb { get => namxb; set => namxb = value; }
        public string Nxb { get => nxb; set => nxb = value; }
        public string Hinhthuc { get => hinhthuc; set => hinhthuc = value; }
        public int Soluong { get => soluong; set => soluong = value; }
        public int Danhgia { get => danhgia; set => danhgia = value; }
        public int Sobinhchon { get => sobinhchon; set => sobinhchon = value; }

        public int Soluongban { get => soluongban; set => soluongban = value; }



        public Book()
        {
        }
    }
}
