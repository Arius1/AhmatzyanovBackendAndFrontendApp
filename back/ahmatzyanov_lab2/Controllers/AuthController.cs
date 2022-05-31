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
using System.Net;

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
        public struct newUser
        {
            public string login { get; set; }
            public string password { get; set; }
            public RoleNames role { get; set; }
        }
        public struct newhash
        {
            public string hash { get; set; }
        }
        private IEnumerable<Claim> claims;

        [HttpPost("token")]
        public object Token(UserToFind userToFind)
        {
            var identity = GetIdentity(userToFind.login, userToFind.password);
            if (identity == null)
            {
                return new { message = "User or password incorrect!" };
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

            User userToFindRole;
            using (AuthContext authContext = new AuthContext())
            {
                userToFindRole = (User)authContext.Users.Where(u => u.Login == userToFind.login).FirstOrDefault();
            }

            return new { access_token = encodedJwt, id = userToFindRole.Id, user_name = userToFind.login, role =  userToFindRole.Role};
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

        [HttpPost("auth/newUser")]
        [Authorize(Roles = "0")]
        public void NewUser(newUser user)
        {
            User newUser = new User();
            newUser.Login = user.login;
            newUser.hashPass = User.StringToByteArray(user.password);
            newUser.Role = user.role;

            using(AuthContext authContext = new AuthContext())
            {
                authContext.Users.Add(newUser);
                authContext.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<object> GetUsers()
        {
            using (AuthContext authContext = new AuthContext())
            {
                var q = from users in authContext.Users
                        select new { id = users.Id, login = users.Login, role = users.Role.ToString() };
                return q.ToList();
            }
        }
        [HttpGet("byId/{id}")]
        public User GetUser(int id)
        {
            using (AuthContext authContext = new AuthContext())
            {
                return authContext.Users.Find(id);
            }
        }
        [Authorize(Roles = "0")]
        [HttpDelete("auth/deleteUser/{id}")]
        public void DeleteUser(int id)
        {
            using (AuthContext authContext = new AuthContext())
            {
                var q = authContext.Users.Find(id);
                authContext.Remove(q);
                authContext.SaveChanges();
            }
        }
        [Authorize]
        [HttpPost("auth/changepass/{id}")]
        public void changePass(int id, newhash newhashpass)
        {
            using (AuthContext authContext = new AuthContext())
            {
                var q = authContext.Users.Find(id);
                q.hashPass = User.StringToByteArray(newhashpass.hash);
                authContext.SaveChanges();
            }
        }
    }
}
