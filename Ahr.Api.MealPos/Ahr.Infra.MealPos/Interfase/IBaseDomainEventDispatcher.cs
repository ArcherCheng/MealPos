using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr
{
    public interface IBaseDomainEventDispatcher
    {
        void Dispatch(BaseDomainEvent domainEvent);
    }
}
