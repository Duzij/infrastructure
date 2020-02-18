using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.ApplicationLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Library.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBookFacade bookFacade;

        public IList<LibraryRecord> LibraryRecords { get; set; }



        public IndexModel(ILogger<IndexModel> logger, IBookFacade bookFacade)
        {
            _logger = logger;
            this.bookFacade = bookFacade;
        }

        public void OnGet()
        {
            bookFacade.Create(new BookCreateDTO() { Title = "test" });

            LibraryRecords = new List<LibraryRecord>()
            {
                new LibraryRecord(){Id=1,BookCount = 1, State =LendRecordState.Lend, UserName = "Test" },
                new LibraryRecord(){Id=2,BookCount = 1, State =LendRecordState.Lend, UserName = "Test" },
                new LibraryRecord(){Id=3,BookCount = 1, State =LendRecordState.Lend, UserName = "Test" },
                new LibraryRecord(){Id=4,BookCount = 1, State =LendRecordState.Lend, UserName = "Test" }
            };
        }
    }

    public class LibraryRecord
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int BookCount { get; set; }

        public LendRecordState State { get; set; }
    }

    public enum LendRecordState
    {
        Lend,
        Returned
    }
}
