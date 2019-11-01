using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr.Infra
{
    public interface IBaseDomainEventDispatcher
    {
        void Dispatch(BaseDomainEvent domainEvent);
    }
}
