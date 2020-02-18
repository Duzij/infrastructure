using System;
using System.Threading.Tasks;

namespace Library.ApplicationLayer
{
    public interface IBookFacade
    {
        Task Create(BookCreateDTO record);
    }
}
