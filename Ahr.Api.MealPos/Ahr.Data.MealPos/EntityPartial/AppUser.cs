using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ahr.Data.MealPos
{
    public partial class AppUser : BaseEntity, IBaseEntity, IValidatableObject
    {

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(this.UserName))
            {
                yield return new ValidationResult("使用者姓名必須輸入", new string[] { "UserName" });
            }
        }
    }
}
