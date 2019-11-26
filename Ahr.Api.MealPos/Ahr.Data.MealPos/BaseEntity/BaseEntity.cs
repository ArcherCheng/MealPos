using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr.Data
{
    public class BaseEntity //:BaseEntity, IBaseEntity, IValidatableObject
    {
        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (this.BirthYear < System.DateTime.Now.Year - 72)
        //    {
        //        yield return new ValidationResult("必須在70歲以內", new string[] { "BirthYear" });
        //    }
        //    if (this.BirthYear > System.DateTime.Now.Year - 15)
        //    {
        //        yield return new ValidationResult("必須在15歲以上", new string[] { "BirthYear" });
        //    }
        //}
    }
}
