using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core;
using Library.ApplicationLayer.DTO;
using Library.ApplicationLayer.Query;
using Library.Domain;

namespace Library.ApplicationLayer
{
    public class LibraryRecordFacade : ILibraryRecordFacade
    {
        private readonly IRepository<LibraryRecord, string> libraryRecordRepository;
        private readonly AllLibraryRecordsQuery allLibraryRecordsQuery;
        private readonly AllLibraryRecordDetailsQuery allLibraryRecordDetailsQuery;
        private readonly IRepository<User, string> userRepository;
        private readonly IRepository<Book, string> bookRepository;

        public LibraryRecordFacade(IRepository<LibraryRecord, string> libraryRecordRepository, AllLibraryRecordDetailsQuery allLibraryRecordDetailsQuery, AllLibraryRecordsQuery allLibraryRecordsQuery, IRepository<User, string> userRepository, IRepository<Book, string> bookRepository)
        {
            this.libraryRecordRepository = libraryRecordRepository;
            this.allLibraryRecordsQuery = allLibraryRecordsQuery;
            this.allLibraryRecordDetailsQuery = allLibraryRecordDetailsQuery;
            this.userRepository = userRepository;
            this.bookRepository = bookRepository;
        }
        public async Task Create(LibraryRecordCreateDTO libraryRecordDto)
        {
            var user = await userRepository.GetByIdAsync(libraryRecordDto.userId);

            List<BookRecord> books = new List<BookRecord>();
            foreach (var book in libraryRecordDto.books)
            {
                var amount = Convert.ToInt32(book.amount);
                var bookId = new BookId(book.id);

                await CheckBookAvailibility(bookId, book.title,
                    amount);

                books.Add(new BookRecord(bookId , new BookAmount(amount), new BookTitle(book.title)));
            }

            var libraryRecord = LibraryRecord.Create(user, books);
            await libraryRecordRepository.InsertNewAsync(libraryRecord);
        }

        private async Task CheckBookAvailibility(BookId bookId, string bookName, int amount)
        {
            var book = await bookRepository.GetByIdAsync(bookId.Value);
            if (book.Amount.Amount < amount)
            {
                throw new ArgumentException($"Book with title {bookName} is not in stock");
            }
        }

        public async Task<List<LibraryRecordDetailDTO>> GetLibraryRecords()
        {
            var libraryRecords = await allLibraryRecordDetailsQuery.GetResultsAsync();
            return libraryRecords.ToList();
        }

        public async Task UpdateLibraryRecordsWithNewBookTitleAsync(string bookId, string newTitle)
        {
            var libraryRecords = await allLibraryRecordsQuery.GetResultsAsync();
            foreach (var record in libraryRecords)
            {
                if (record.Books.Any(a => a.BookId.Value == bookId))
                {
                    await libraryRecordRepository.ModifyAsync(record => { record.UpdateBookTitle(bookId, newTitle); }, record.Id.Value);
                }
            }
        }


        public async Task<LibraryRecordDetailDTO> GetLibraryRecordById(string id)
        {
            return LibraryRecordConverter.Convert(await libraryRecordRepository.GetByIdAsync(id));
        }

        public async Task ReturnBookAsync(string recordId, string bookId, int amountValue)
        {
            await libraryRecordRepository.ModifyAsync(record => { record.ReturnBook(new BookId(bookId), new BookAmount(amountValue)); }, recordId);
        }
    }
}
