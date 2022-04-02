using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Model.Models
{
   public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Icon { get; set; }
        public string Nick { get; set; }
        public string Email { get; set; }
        public string Ip { get; set; }
        public int? Age { get; set; }
        public string Introduction { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

    }
}
