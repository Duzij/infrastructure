using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Core
{
    public interface IEventHandler<TEvent>
    {
        void Handle(TEvent @event);
    }
}
