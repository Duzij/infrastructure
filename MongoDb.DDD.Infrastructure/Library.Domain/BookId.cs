﻿using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class BookId : IId<Guid>
    {
        public Guid Value { get; set; }

        public BookId(Guid value)
        {
            Value = value;
        }
    }
}
