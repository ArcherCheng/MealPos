using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ahr.Service.MealPos
{
    public class UserToReturnDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string MainPhotoUrl { get; set; }

    }
}
