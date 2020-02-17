using System;

namespace Library.ApplicationLayer
{
    public interface IBookFacade
    {
        void Create(BookCreateDTO record);
    }
}
