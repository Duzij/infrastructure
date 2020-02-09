using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Core
{
    public interface ICommandHandler
    {
        void Hadle(object command);
    }
}
