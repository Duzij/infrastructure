using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class LibraryRecord : IEntity<string>
    {
        public ICollection<Book> Books { get; set; }
        public string Id { get; set; }

        public void AddEvent(object @event)
        {
            throw new NotImplementedException();
        }

        public bool CheckState()
        {
            throw new NotImplementedException();
        }

        public IList<object> GetEvents()
        {
            throw new NotImplementedException();
        }
    }
}
