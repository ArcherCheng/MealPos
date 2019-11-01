using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ahr.Api.MealPos
{
    public class UserToReturnDto
    {
        public int UserId { get; set; }
        public string NickName { get; set; }
        public string UserRole { get; set; }
        public string MainPhotoUrl { get; set; }

    }
}
