using Ahr.Data.MealPos;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Ahr.Service.MealPos
{
    public class AuthService : AppBaseService, IAuthService
    {
        private readonly IMapper _mapper;

        public AuthService(IMapper mapper)
        {
            this._mapper = mapper;
        }
        public async Task<bool> ChangePassword(ChangePasswordDto model)
        {
            var user = await GetUser(model.Email, model.Phone);
            if (user.Email != model.Email || user.Phone != model.Phone )
                return false;

            using (var db = base.NewDb())
            {
                //先判定原密碼是否正確
                if (!PasswordHash.VerifyPasswordHash(model.OldPassword, user.PasswordHash, user.PasswordSalt))
                    return false;

                byte[] passwordHash, passwordSalt;
                PasswordHash.CreatePasswordHash(model.NewPassword, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.WriteTime = System.DateTime.Now;
                db.AppUser.Update(user);
                var saveNumber = await db.SaveChangesAsync();
                return saveNumber > 0;
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

        public async Task<AppUser> GetUser(string email, string phone)
        {
            using (var db = base.NewDb())
            {
                var result = await db.AppUser
                    .FirstOrDefaultAsync(x => x.Email == email && x.Phone == phone);
                return result;
            }
        }

        public async Task<UserToReturnDto> Login(string emailOrPhone, string password)
        {
            using (var db = base.NewDb())
            {
                var user = await db.AppUser
                    .FirstOrDefaultAsync(x => x.Email == emailOrPhone || x.Phone == emailOrPhone);
                if (user == null)
                    return null;

                if (!PasswordHash.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                    return null;

                user.LoginDate = System.DateTime.Now;
                db.AppUser.Update(user);
                var saveNumber = await db.SaveChangesAsync();
                var dto = _mapper.Map<AppUser, UserToReturnDto>(user);
                return dto;
            }
        }

        public async Task<string> NewPassword(ForgetPasswordDto model)
        {
            using (var db = base.NewDb())
            {
                var user = await GetUser(model.Email, model.Phone);
                if (user.Email != model.Email || user.Phone != model.Phone || user.UserName != model.UserName)
                    return "";

                var newPass = new System.Random();
                var newPassword = newPass.Next(111111, 999999).ToString();

                byte[] passwordHash, passwordSalt;
                PasswordHash.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                db.AppUser.Update(user);
                var saveNumber = await db.SaveChangesAsync();

                return newPassword;
            }
        }

        public async Task<UserToReturnDto> Register(RegisterDto registerDto, string password)
        {
            var user = _mapper.Map<RegisterDto, AppUser>(registerDto);
            byte[] passwordHash, passwordSalt;
            PasswordHash.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.IsInWork = true;
            user.LoginDate = System.DateTime.Now;
            user.UserRole = "users";
            using (var db = base.NewDb())
            {
                db.AppUser.Add(user);
                var saveNumber = await db.SaveChangesAsync();
                return _mapper.Map<AppUser,UserToReturnDto>(user);
            }
        }

        public async Task<bool> UserIsExists(string userEmail, string userPhone)
        {
            using (var db = base.NewDb())
            {
                var user = await db.AppUser
                    .FirstOrDefaultAsync(p => p.Email == userEmail || p.Phone == userPhone);
                if (user == null)
                    return false;
                return true;
            }
        }

        //public string UserLoginToken(AppUser AppUser, string tokenSecretKey)
        //{
        //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenSecretKey));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, AppUser.Id.ToString()),
        //        new Claim(ClaimTypes.Name,AppUser.UserName),
        //        new Claim(ClaimTypes.Role,AppUser.UserRole ?? "users" )
        //    };

        //    var tokenDescripter = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(claims),
        //        Expires = System.DateTime.Now.AddDays(30),
        //        SigningCredentials = creds
        //    };

        //    var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        //    var token = tokenHandler.CreateToken(tokenDescripter);
        //    var tokenResult = tokenHandler.WriteToken(token);
        //    return tokenResult;
        //    //return "";
        //}
    }
}
