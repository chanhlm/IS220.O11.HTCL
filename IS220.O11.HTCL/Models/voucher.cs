using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS220.O11.HTCL.Models
{
    public class voucher
    {
        private string  img, loai, manhap, noidung;
        private int daluu, sl, phantram, dieukien;
        private DateTime ngaybd, ngaykt;
        private int makm;

        public int Makm { get => makm; set => makm = value; }
        public string Img { get => img; set => img = value; }
        public string Loai { get => loai; set => loai = value; }
        public string Manhap { get => manhap; set => manhap = value; }
        public string Noidung { get => noidung; set => noidung = value; }
        public int Daluu { get => daluu; set => daluu = value; }
        public int Sl { get => sl; set => sl = value; }
        public int Phantram { get => phantram; set => phantram = value; }
        public int Dieukien { get => dieukien; set => dieukien = value; }
        public DateTime Ngaybd { get => ngaybd; set => ngaybd = value; }
        public DateTime Ngaykt { get => ngaykt; set => ngaykt = value; }
        public voucher() {  }

        public voucher(int makm, string img, string loai, string manhap, string noidung, int daluu, int sl, int phantram, int dieukien, DateTime ngaybd, DateTime ngaykt)
        {
            Makm = makm;
            Img = img;
            Loai = loai;
            Manhap = manhap;
            Noidung = noidung;
            Daluu = daluu;
            Sl = sl;
            Phantram = phantram;
            Dieukien = dieukien;
            Ngaybd = ngaybd;
            Ngaykt = ngaykt;
        }
    }
}
