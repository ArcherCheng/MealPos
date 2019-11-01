using Ahr.Data.MealPos;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ahr.Service.MealPos
{
    public class AuthService : AppBaseService, IAuthService
    {
        public async Task<bool> ChangePassword(SysUser user, string oldPassword, string newPassword)
        {
            using (var db = base.NewDb())
            {
                //先判定原密碼是否正確
                if (!PasswordHash.VerifyPasswordHash(oldPassword, user.PasswordHash, user.PasswordSalt))
                    return false;

                byte[] passwordHash, passwordSalt;
                PasswordHash.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.UpdateTime = System.DateTime.Now;
                await db.SaveChangesAsync();
                return true;
            }
        }

        //public async Task<IEnumerable<GroupKeyValue>> GetGroupKeyValueList(string keyGroup)
        //{
        //    using (var db = base.NewDb())
        //    {
        //        var list = await db.GroupKeyValue
        //            .Where(x => x.KeyGroup == keyGroup)
        //            .OrderBy(x => x.KeySeq)
        //            .ToListAsync();
        //        return list;
        //    }
        //}

        public async Task<SysUser> GetUser(string email, string phone)
        {
            using (var db = base.NewDb())
            {
                var result = await db.SysUser
                    .FirstOrDefaultAsync(x => x.Email == email && x.Phone == phone);
                return result;
            }
        }

        public async Task<SysUser> Login(string emailOrPhone, string password)
        {
            using (var db = base.NewDb())
            {
                var user = await db.SysUser
                    .FirstOrDefaultAsync(x => x.Email == emailOrPhone || x.Phone == emailOrPhone);
                if (user == null)
                    return null;

                if (!PasswordHash.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                    return null;

                user.LoginDate = System.DateTime.Now;
                await db.SaveChangesAsync();

                return user;
            }
        }

        public async Task<string> NewPassword(SysUser user)
        {
            using (var db = base.NewDb())
            {
                var newPass = new System.Random();
                var newPassword = newPass.Next(111111, 999999).ToString();

                byte[] passwordHash, passwordSalt;
                PasswordHash.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                await db.SaveChangesAsync();

                return newPassword;
            }
        }

        public async Task<SysUser> Register(SysUser user, string password)
        {
            byte[] passwordHash, passwordSalt;
            PasswordHash.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            using (var db = base.NewDb())
            {
                db.SysUser.Add(user);
                await db.SaveChangesAsync();
                return user;
            }

        }

        public async Task<bool> UserIsExists(string userEmail, string userPhone)
        {
            using (var db = base.NewDb())
            {
                var user = await db.SysUser
                    .FirstOrDefaultAsync(p => p.Email == userEmail || p.Phone == userPhone);
                if (user == null)
                    return false;
                return true;
            }
        }

        public string UserLoginToken(SysUser SysUser, string tokenSecretKey)
        {
            //var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenSecretKey));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //var claims = new[]
            //{
            //    new Claim(ClaimTypes.NameIdentifier, SysUser.Id.ToString()),
            //    new Claim(ClaimTypes.Name,SysUser.UserName),
            //    new Claim(ClaimTypes.Role,SysUser.UserRole ?? "users" )
            //};

            //var tokenDescripter = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(claims),
            //    Expires = System.DateTime.Now.AddDays(30),
            //    SigningCredentials = creds
            //};

            //var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            //var token = tokenHandler.CreateToken(tokenDescripter);
            //var tokenResult = tokenHandler.WriteToken(token);
            //return tokenResult;
            return "";
        }
    }
}
