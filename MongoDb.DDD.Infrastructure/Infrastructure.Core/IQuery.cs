using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Core
{
    public interface IQuery<TResult>
    {
        public abstract Task<IList<TResult>> GetResultsAsync();
    }
}
