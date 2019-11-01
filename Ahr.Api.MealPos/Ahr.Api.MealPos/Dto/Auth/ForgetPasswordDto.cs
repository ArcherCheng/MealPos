using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ahr.Api.MealPos
{
    public class ForgetPasswordDto
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

    }
}
