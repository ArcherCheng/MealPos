using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr.Infra
{
    public interface IHandle<T> where T:BaseDomainEvent
    {
        void Handle(T domainEvent);
    }
}
