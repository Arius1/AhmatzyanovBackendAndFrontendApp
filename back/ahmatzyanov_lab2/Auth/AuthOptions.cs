using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ahmatzyanov_lab2.Auth
{
    public class AuthOptions
    {
        public const string Issuer = "GS_Server"; // издатель токена
        public const string Audience = "MyAuthClient"; // потребитель токена
        const string Key = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LifeTime = 5; // время жизни токена
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
