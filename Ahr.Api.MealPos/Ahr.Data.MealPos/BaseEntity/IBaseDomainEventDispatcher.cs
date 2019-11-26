using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr.Data
{
    public interface IBaseDomainEventDispatcher
    {
        void Dispatch(BaseDomainEvent domainEvent);
    }
}
