using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS220.O11.HTCL.Models
{
    public class comment
    {
        private string matk, masach, binhluan;
        private int sosao;
        private DateTime ngaybl;

        public string Matk { get => matk; set => matk = value; }
        public string Masach { get => masach; set => masach = value; }
        public string Binhluan { get => binhluan; set => binhluan = value; }
        public int Sosao { get => sosao; set => sosao = value; }
        public DateTime Ngaybl { get => ngaybl; set => ngaybl = value; }

        public comment() {  }

        public comment(string MATK, string MASACH, string BINHLUAN, DateTime NGAYBL, int SOSAO)
        {
            this.matk = MATK;
            this.masach = MASACH; ;
            this.binhluan = BINHLUAN;
            this.ngaybl = NGAYBL;
            this.sosao = SOSAO;
        }
    }
}
