using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS220.O11.HTCL.Models
{
    public class detail_order
    {
        private int madh;
        private int masach;
        private int soluong;

        public int Madh { get => madh; set => madh = value; }
        public int Masach { get => masach; set => masach = value; }
        public int Soluong { get => soluong; set => soluong = value; }

        public detail_order() { }

        public detail_order(int madh, int masach, int soluong)
        {
            this.madh = madh;
            this.masach = masach;
            this.soluong = soluong;
        }
    }
}