using Infrastructure.Core;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ApplicationLayer.Query
{
    public interface IAuthorByBookTitleQuery : IQuery<Author, AuthorDetailDTO>
    {
        public string BookTitle { get; set; }
    }
}
