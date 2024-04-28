using Infrastructure.Core;
using Library.ApplicationLayer.DTO;
using Library.ApplicationLayer.Mappers;
using Library.ApplicationLayer.Query;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer
{
    public class LibraryRecordFacade : ILibraryRecordFacade
    {
        private readonly IRepository<LibraryRecord, string> libraryRecordRepository;
        private readonly AllLibraryRecordsQuery allLibraryRecordsQuery;
        private readonly ValidLibraryRecordDetailsQuery validLibraryRecordDetailsQuery;
        private readonly IRepository<User, string> userRepository;
        private readonly IRepository<Book, string> bookRepository;

        public LibraryRecordFacade(IRepository<LibraryRecord, string> libraryRecordRepository, ValidLibraryRecordDetailsQuery validRecordDetailsQuery, AllLibraryRecordsQuery allLibraryRecordsQuery, IRepository<User, string> userRepository, IRepository<Book, string> bookRepository)
        {
            this.libraryRecordRepository = libraryRecordRepository;
            this.allLibraryRecordsQuery = allLibraryRecordsQuery;
            this.validLibraryRecordDetailsQuery = validRecordDetailsQuery;
            this.userRepository = userRepository;
            this.bookRepository = bookRepository;
        }

        public async Task<List<LibraryRecordId>> GetLibraryRecordsByUserAsync(UserRecord user)
        {
            var query = allLibraryRecordsQuery;
            query.Filter = (a => a.User == user);

            var records = await query.GetResultsAsync();

            return records.Select(a => (LibraryRecordId)a.Id).ToList();
        }

        public async Task Create(LibraryRecordCreateDTO libraryRecordDto)
        {
            var user = await userRepository.GetByIdAsync(new UserId(libraryRecordDto.userId));

            List<BookRecord> books = new List<BookRecord>();
            foreach (var book in libraryRecordDto.books)
            {
                var amount = Convert.ToInt32(book.amount);
                var bookId = new BookId(book.id);

                await CheckBookAvailibility(bookId, book.title,
                    amount);

                books.Add(new BookRecord(bookId, new BookAmount(amount), new BookTitle(book.title)));
            }

            var libraryRecord = LibraryRecord.Create(UserMapper.MapTo(user), books);
            await libraryRecordRepository.InsertNewAsync(libraryRecord);
        }

        private async Task CheckBookAvailibility(BookId bookId, string bookName, int amount)
        {
            var book = await bookRepository.GetByIdAsync(bookId);
            if (book.Amount.Amount < amount)
            {
                throw new ArgumentException($"Book with title {bookName} is not in stock");
            }
        }

        public async Task<List<LibraryRecordDetailDTO>> GetLibraryRecords()
        {
            var libraryRecords = await validLibraryRecordDetailsQuery.GetResultsAsync();
            return libraryRecords.ToList();
        }

        public async Task UpdateLibraryRecordsWithNewBookTitleAsync(string bookId, string newTitle)
        {
            var libraryRecords = await allLibraryRecordsQuery.GetResultsAsync();
            foreach (var record in libraryRecords)
            {
                if (record.Books.Any(a => a.BookId.Value == bookId))
                {
                    await libraryRecordRepository.ModifyAsync(record => { record.UpdateBookTitle(bookId, newTitle); }, record.Id);
                }
            }
        }

        public async Task<LibraryRecordDetailDTO> GetLibraryRecordById(string id)
        {
            return LibraryRecordMapper.MapTo(await libraryRecordRepository.GetByIdAsync(new LibraryRecordId(id)));
        }

        public async Task ReturnBookAsync(ReturnBookDTO returnBookDTO)
        {
            await libraryRecordRepository.ModifyAsync(record => { record.ReturnBook(new BookId(returnBookDTO.bookId), new BookAmount(Convert.ToInt32(returnBookDTO.bookAmount))); }, new LibraryRecordId(returnBookDTO.libraryRecordId));
        }

        public async Task PayLibraryRecord(string id)
        {
            await libraryRecordRepository.ModifyAsync(record => { record.PayFine(); }, new LibraryRecordId(id));
        }

        public async Task<List<LibraryRecordDetailDTO>> GetAllLibraryRecords()
        {
            var records = await allLibraryRecordsQuery.GetResultsAsync();
            var returnRecords = new List<LibraryRecordDetailDTO>();
            foreach (var item in records)
            {
                returnRecords.Add(LibraryRecordMapper.MapTo(item));
            }
            return returnRecords;
        }
    }
}
