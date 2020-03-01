using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core;
using Library.ApplicationLayer.DTO;
using Library.Domain;

namespace Library.ApplicationLayer
{
    public class LibraryRecordFacade : ILibraryRecordFacade
    {
        private readonly IRepository<LibraryRecord, string> libraryRecordRepository;
        private readonly IRepository<User, string> userRepository;

        public LibraryRecordFacade(IRepository<LibraryRecord, string> libraryRecordRepository, IRepository<User, string> userRepository)
        {
            this.libraryRecordRepository = libraryRecordRepository;
            this.userRepository = userRepository;
        }
        public async Task Create(LibraryRecordCreateDTO libraryRecordDto)
        {
            var user = await userRepository.GetByIdAsync(libraryRecordDto.UserId);

            var libraryRecord = LibraryRecord.Create(user);

            await libraryRecordRepository.InsertNewAsync(libraryRecord);
        }
    }
}
