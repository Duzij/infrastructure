using Infrastructure.Core;
using Library.ApplicationLayer.Mappers;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer.EventHandlers
{
    public class UserUpdatedEventHandler : IEventHandler<UserUpdated>
    {
        private readonly IRepository<LibraryRecord, string> repository;
        private readonly ILibraryRecordFacade libraryRecordFacade;

        public UserUpdatedEventHandler(IRepository<LibraryRecord, string> repository, ILibraryRecordFacade libraryRecordFacade)
        {
            this.repository = repository;
            this.libraryRecordFacade = libraryRecordFacade;
        }
        public async Task Handle(UserUpdated @event)
        {
            var affectedRecords = await libraryRecordFacade.GetLibraryRecordsByUserAsync(@event.oldUser);

            foreach (var recordId in affectedRecords)
            {
                await repository.ModifyAsync(a => a.UpdateUser(@event.updatedUser), recordId);
            }
        }
    }
}
