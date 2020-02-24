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

        public IList<LibraryRecordDTO> LibraryRecords { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IBookFacade bookFacade)
        {
            _logger = logger;
            this.bookFacade = bookFacade;
        }

        public void OnGet()
        {
            bookFacade.Create(new BookCreateDTO() { Title = "test" });

            LibraryRecords = new List<LibraryRecordDTO>()
            {
                new LibraryRecordDTO(){Id="1", State =LendRecordState.Lend, UserName = "Test" },
                new LibraryRecordDTO(){Id="2", State =LendRecordState.Lend, UserName = "Test" },
                new LibraryRecordDTO(){Id="3", State =LendRecordState.Lend, UserName = "Test" },
                new LibraryRecordDTO(){Id="4", State =LendRecordState.Lend, UserName = "Test" }
            };
        }
    }

    public class LibraryRecordDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int BookCount => Books.Count;
        public List<string> Books { get; set; } = new List<string>();
        public LendRecordState State { get; set; }
    }

    public enum LendRecordState
    {
        Lend,
        Returned
    }
}
