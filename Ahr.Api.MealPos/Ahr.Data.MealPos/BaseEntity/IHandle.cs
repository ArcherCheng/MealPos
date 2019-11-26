using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr.Data
{
    public interface IHandle<T> where T : BaseDomainEvent
    {
        void Handle(T domainEvent);
    }
}
