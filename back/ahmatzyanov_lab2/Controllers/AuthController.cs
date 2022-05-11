using ahmatzyanov_lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using ahmatzyanov_lab2.Auth;
using ahmatzyanov_lab2.Contexts;
using Microsoft.AspNetCore.Authorization;

namespace ahmatzyanov_lab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController
    {
        public struct UserToFind
        {
            public string login { get; set; }
            public string password { get; set; }
        }
        private IEnumerable<Claim> claims;

        [HttpPost("token")]
        public object Token(UserToFind userToFind)
        {
            var identity = GetIdentity(userToFind.login, userToFind.password);
            if (identity == null)
            {
                return new { message = "wrong login/password" };
            }
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // создаем JWT-токен
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    notBefore: now,
                    expires: now.AddMinutes(AuthOptions.LifeTime),
                    claims: identity.Claims,
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)); ;
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new { access_token = encodedJwt, user_name = userToFind.login };
        }


        private ClaimsIdentity GetIdentity(string login, string password)
        {
            using (AuthContext authContext = new AuthContext())
            {

                User person = authContext.Users.FirstOrDefault(x => x.Login == login);

                if (person != null && person.CheckPassword(password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, person.IsAdmin?"0":"1")
                    };
                    ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }
                return null;
            }
        }

        [HttpPost]
        [Authorize(Roles = "0")]
        public void NewUser(User user)
        {
            using(AuthContext authContext = new AuthContext())
            {
                authContext.Users.Add(user);
                authContext.SaveChanges();
            }
        }
        [HttpGet]
        public List<User> GetUsers()
        {
            using (AuthContext authContext = new AuthContext())
            {
                return authContext.Users.ToList();
            }
        }
    }
}
