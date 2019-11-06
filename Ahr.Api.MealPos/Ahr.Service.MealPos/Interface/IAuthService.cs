using Ahr.Data.MealPos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ahr.Service.MealPos
{
    public interface IAuthService : IAppBaseService
    {
        Task<AppUser> Register(AppUser user, string password);
        Task<AppUser> Login(string userEmail, string password);
        Task<bool> UserIsExists(string emailOrPhone, string userPhone);
        Task<AppUser> GetUser(string email, string phone);
        //Task<IEnumerable<GroupKeyValue>> GetGroupKeyValueList(string keyGroup);
        Task<string> NewPassword(AppUser user);
        Task<bool> ChangePassword(AppUser user, string oldPassword, string newPassword);
        //string UserLoginToken(AppUser user, string tokenSecretKey);
    }
}
