using Library.ApplicationLayer.DTO;
using Library.Domain;
using Library.Domain.Id;

namespace Library.ApplicationLayer
{
    public interface ILibraryRecordFacade
    {
        Task Create(LibraryRecordCreateDTO libraryRecordDto);

        Task<List<LibraryRecordDetailDTO>> GetLibraryRecords();
        Task UpdateLibraryRecordsWithNewBookTitleAsync(string bookId, string newTitle);
        Task<LibraryRecordDetailDTO> GetLibraryRecordById(string id);
        Task ReturnBookAsync(ReturnBookDTO returnBookDTO);
        Task PayLibraryRecord(string id);
        Task<List<LibraryRecordDetailDTO>> GetAllLibraryRecords();
        Task<List<LibraryRecordId>> GetLibraryRecordsByUserAsync(UserRecord user);
    }
}
