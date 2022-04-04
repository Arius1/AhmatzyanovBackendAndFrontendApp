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
        [JsonIgnore]
        public byte[] hashPass { get; set; }
        [NotMapped]
        [JsonIgnore]
        public string Password
        {
            get
            {
                return null;
            }
            set { hashPass = new SHA256CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(value)); }
        }
        public bool IsAdmin => Role == RoleNames.Admin;
        public bool CheckPassword(string password)
        {
            byte[] checkedHash = new SHA256CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(password));
            return checkedHash.SequenceEqual(this.hashPass);

        }
    }
}
