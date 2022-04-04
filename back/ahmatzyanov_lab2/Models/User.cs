using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using System.Threading.Tasks;

namespace ahmatzyanov_lab2.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Login { get; set; }
        public RoleNames Role { get; set; }
        //private byte[] password;
        //[JsonIgnore]
        //public string Password
        //{
        //    get
        //    {
        //        return password.ToString();
        //    }
        //    set { password = new SHA256CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(value)); }
        //}
        public bool IsAdmin => Role == RoleNames.Admin;
        //public bool CheckPassword(string password) => new SHA256CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(password)) == this.password;
        public string Password { get; set; }
    }
}
