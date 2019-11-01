using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr
{
    public interface IBaseEntity
    {
        int Id { get; set; }

        DateTime? CreateTime { get; set; }

        DateTime? UpdateTime { get; set; }

        string WriteUid { get; set; }

        string WriteIp { get; set; }
    }
}
