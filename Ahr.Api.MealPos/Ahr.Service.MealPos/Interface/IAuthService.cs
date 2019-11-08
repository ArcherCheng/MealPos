using Ahr.Data.MealPos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ahr.Service.MealPos
{
    public interface IAuthService : IAppBaseService
    {
        Task<UserToReturnDto> Register(RegisterDto user, string password);
        Task<UserToReturnDto> Login(string userEmail, string password);
        Task<bool> UserIsExists(string emailOrPhone, string userPhone);
        Task<AppUser> GetUser(string email, string phone);
        //Task<IEnumerable<GroupKeyValue>> GetGroupKeyValueList(string keyGroup);
        Task<string> NewPassword(ForgetPasswordDto user);
        Task<bool> ChangePassword(ChangePasswordDto user);
        //string UserLoginToken(AppUser user, string tokenSecretKey);
    }
}
