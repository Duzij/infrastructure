using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Core
{
    public interface IEventHandler<TEvent>
    {
        Task Handle(TEvent @event);
    }
}
