using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Core
{
    public interface IQuery<TEntity, TResult>
    {
        public IList<TResult> GetResults();
    }
}
