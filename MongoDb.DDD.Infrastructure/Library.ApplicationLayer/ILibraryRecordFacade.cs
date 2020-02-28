using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Library.ApplicationLayer.DTO;

namespace Library.ApplicationLayer
{
    public interface ILibraryRecordFacade
    {
        Task Create(LibraryRecordCreateDTO libraryRecordDto);
    }
}
