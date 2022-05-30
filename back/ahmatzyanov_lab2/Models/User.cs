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
        [JsonIgnore]
        public bool IsAdmin => Role == RoleNames.Admin;
        public bool CheckPassword(string password)
        {
            return StringToByteArray(password).SequenceEqual(this.hashPass);

        }
        public byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
