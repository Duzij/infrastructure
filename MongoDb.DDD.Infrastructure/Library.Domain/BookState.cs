using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Domain
{
    public enum BookState
    {
        Inactive, //is in database, but not on the shelves
        Lent, 
        InStock,
        Destroyed
    }
}
