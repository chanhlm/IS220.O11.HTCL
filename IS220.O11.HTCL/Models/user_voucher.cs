using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS220.O11.HTCL.Models
{
    public class user_voucher
    {
        private int makm;
        private string email;
        public string Email { get => email; set => email = value; }
        public int Makm { get => makm; set => makm = value; }

        public user_voucher() { }
        public user_voucher(string email, int makm)
        {
            this.makm = makm;
            this.email = email;
        }
    }
}
