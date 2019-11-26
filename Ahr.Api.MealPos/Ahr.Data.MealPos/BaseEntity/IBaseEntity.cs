using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr.Data
{
    public interface IBaseEntity
    {
        int Id { get; set; }

        DateTime? WriteTime { get; set; }

        string WriteUser { get; set; }

        string WriteIp { get; set; }
    }
}
