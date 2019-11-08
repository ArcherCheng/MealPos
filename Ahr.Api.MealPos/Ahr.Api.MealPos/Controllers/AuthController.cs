using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ahr.Service.MealPos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Ahr.Api.MealPos.Controllers
{
    public class AuthController : AppControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _service;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;

        public AuthController(IMapper mapper, IAuthService service,
            Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            this._mapper = mapper;
            this._service = service;
            this._config = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _service.UserIsExists(model.Email, model.Phone))
                return BadRequest("此電子郵件或電話已經是會員了");

            var userToReturn = await _service.Register(model, model.password);
            return Ok(userToReturn);
 
            //重新導向使用者資料編輯
            //return CreatedAtRoute("GetAccountr", new {controller = "account", id = userToReturn.USERID}, userToReturn);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userToReturn = await _service.Login(model.Username, model.Password);
            if (userToReturn == null)
                return Unauthorized();

            var tokenKey = _config.GetSection("AppSettings:Token").Value;

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userToReturn.Id.ToString()),
                new Claim(ClaimTypes.Name,userToReturn.UserName),
                new Claim(ClaimTypes.Role,userToReturn.UserRole ?? "users" )
            };

            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(30),
                SigningCredentials = creds
            };

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescripter);

            return Ok(new
            {
                tokenToReturn = tokenHandler.WriteToken(token),
                userToReturn
            });
        }

        [HttpPost("forgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newPassword = await _service.NewPassword(model);
            if(newPassword == "")
                return BadRequest("資枓比對不一致,請重新輸入");

            return Ok(newPassword);
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isChanged = await _service.ChangePassword(model);
            if (!isChanged)
                BadRequest("原密碼輸入錯誤,請重新輸入");

            return Ok();
        }

        //private string UserLoginToken(UserToReturnDto user, string tokenSecretKey)
        //{
        //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenSecretKey));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //        new Claim(ClaimTypes.Name,user.UserName),
        //        new Claim(ClaimTypes.Role,user.UserRole ?? "users" )
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
        //}
    }
}