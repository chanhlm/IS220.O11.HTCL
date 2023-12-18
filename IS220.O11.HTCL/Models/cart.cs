using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS220.O11.HTCL.Models
{
    public class cart
    {
        private string matk, masach;
        private int soluong;

        public string Matk { get => matk; set => matk = value; }
        public string Masach { get => masach; set => masach = value; }
        public int Soluong { get => soluong; set => soluong = value; }

        public cart() { }
        public cart(string matk, string masach, int soluong)
        {
            this.matk = matk;
            this.masach = masach;
            this.soluong = soluong;
        }
    }
}
