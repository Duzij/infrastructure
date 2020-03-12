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

        Task<List<LibraryRecordDetailDTO>> GetLibraryRecords();
        Task UpdateLibraryRecordsWithNewBookTitleAsync(string bookId, string newTitle);
        Task<LibraryRecordDetailDTO> GetLibraryRecordById(string id);
        Task ReturnBookAsync(string recordId, string bookId, int amountValue);
    }
}
