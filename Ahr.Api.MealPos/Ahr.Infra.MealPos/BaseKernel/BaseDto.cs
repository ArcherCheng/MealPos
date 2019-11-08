using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr
{
    public abstract class BaseDto 
    {
        public CrudStatus CrudStatus { get; set; }
    }

    public enum CrudStatus
    {
        Read=0,
        Create,
        Update,
        Delete
    }
}
