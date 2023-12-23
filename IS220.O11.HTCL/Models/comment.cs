using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS220.O11.HTCL.Models
{
    public class comment
    {
        private string email, masach, binhluan;
        private int sosao;
        private DateTime ngaybl;

        public string Email { get => email; set => email = value; }
        public string Masach { get => masach; set => masach = value; }
        public string Binhluan { get => binhluan; set => binhluan = value; }
        public int Sosao { get => sosao; set => sosao = value; }
        public DateTime Ngaybl { get => ngaybl; set => ngaybl = value; }

        public comment() {  }

        public comment(string email, string MASACH, string BINHLUAN, DateTime NGAYBL, int SOSAO)
        {
            this.email = email;
            this.masach = MASACH; ;
            this.binhluan = BINHLUAN;
            this.ngaybl = NGAYBL;
            this.sosao = SOSAO;
        }
    }
}
