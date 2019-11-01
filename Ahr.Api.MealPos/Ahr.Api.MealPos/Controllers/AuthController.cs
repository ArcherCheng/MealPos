using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ahr.Data.MealPos;
using Ahr.Service.MealPos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ahr.Api.MealPos.Controllers
{
    public class AuthController : ApiControllerBase
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
                return BadRequest(ModelState.ValidationState);

            if (await _service.UserIsExists(model.Email, model.Phone))
                return BadRequest("此電子郵件或電話已經是會員了");

            var mapUser = _mapper.Map<SysUser>(model);
            var member = await _service.Register(mapUser, model.password);
            return Ok();

            //返回簡單使用者資料
            // var user = _mapper.Map<UserToReturnDto>(member);
            // return Ok(user);

            //重新導向使用者資料編輯
            //return CreatedAtRoute("GetAccountr", new {controller = "account", id = userToReturn.USERID}, userToReturn);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var member = await _service.Login(model.Username, model.Password);
            if (member == null)
                return Unauthorized();

            var tokenSecretKey = _config.GetSection("AppSettings:Token").Value;

            //var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenSecretKey));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //var claims = new[]
            //{
            //    new Claim(ClaimTypes.NameIdentifier, member.UserId.ToString()),
            //    new Claim(ClaimTypes.Name,member.NickName),
            //    new Claim(ClaimTypes.Role,member.UserRole ?? "users" )
            //};

            //var tokenDescripter = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(claims),
            //    Expires = System.DateTime.Now.AddDays(30),
            //    SigningCredentials = creds
            //};

            //var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            //var token = tokenHandler.CreateToken(tokenDescripter);

            //返回簡單使用者資料
            //var user = _mapper.Map<UserToReturnDto>(member);
            //return Ok(new
            //{
            //    token = tokenHandler.WriteToken(token),
            //    user
            //});

            var tokenResult = _service.UserLoginToken(member, tokenSecretKey);
            var userToReturn = _mapper.Map<UserToReturnDto>(member);
            return Ok(new
            {
                tokenResult,
                userToReturn
            });
        }

        [HttpPost("forgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ValidationState);

            var member = await _service.GetUser(model.Email, model.Phone);
            if (member.Email != model.Email || member.Phone != model.Phone || member.UserName != model.UserName)
                return BadRequest("資枓比對不一致,請重新輸入");

            var newPassword = await _service.NewPassword(member);

            return Ok(newPassword);
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ValidationState);

            var member = await _service.GetUser(model.Email, model.Phone);

            if (member.Email != model.Email || member.Phone != model.Phone)
                return BadRequest("電話或電子郵件輸入錯誤,請重新輸入");

            var isChanged = await _service.ChangePassword(member, model.OldPassword, model.NewPassword);

            if (!isChanged)
                BadRequest("原密碼輸入錯誤,請重新輸入");

            return Ok();
        }

    }
}