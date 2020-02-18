using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Core
{
    public interface IEventHandler<TEntityKey>
    {
        void Hadle(IEvent<TEntityKey> @event);
    }
}
