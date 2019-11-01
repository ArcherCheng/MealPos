using Ahr.Data.MealPos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ahr.Service.MealPos
{
    public interface IAuthService : IBaseService
    {
        Task<SysUser> Register(SysUser user, string password);
        Task<SysUser> Login(string userEmail, string password);
        Task<bool> UserIsExists(string emailOrPhone, string userPhone);
        Task<SysUser> GetUser(string email, string phone);
        //Task<IEnumerable<GroupKeyValue>> GetGroupKeyValueList(string keyGroup);
        Task<string> NewPassword(SysUser user);
        Task<bool> ChangePassword(SysUser user, string oldPassword, string newPassword);
        string UserLoginToken(SysUser user, string tokenSecretKey);
    }
}
