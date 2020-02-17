using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class LibraryRecord : Entity
    {
        public ICollection<Book> Books { get; set; }

        public override void Apply(object @event)
        {
            throw new NotImplementedException();
        }

        public override bool CheckState()
        {
            throw new NotImplementedException();
        }
    }
}
