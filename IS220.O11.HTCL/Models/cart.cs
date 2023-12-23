using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS220.O11.HTCL.Models
{
    public class cart
    {
        private string email, masach;
        private int soluong;

        public string Email { get => email; set => email = value; }
        public string Masach { get => masach; set => masach = value; }
        public int Soluong { get => soluong; set => soluong = value; }

        public cart() { }
        public cart(string email, string masach, int soluong)
        {
            this.email = email;
            this.masach = masach;
            this.soluong = soluong;
        }
    }
}
