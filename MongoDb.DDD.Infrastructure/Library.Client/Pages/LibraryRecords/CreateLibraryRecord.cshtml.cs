using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.ApplicationLayer;
using Library.ApplicationLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Library.Client.Pages.LibraryRecords
{
    public class CreateLibraryRecord : PageModel
    {
        private readonly ILogger<CreateLibraryRecord> logger;
        private readonly IBookFacade bookFacade;
        private readonly ILibraryRecordFacade libraryRecordFacade;

        public LibraryRecordCreateDTO LibraryRecordDto { get; set; }

        public List<SelectListItem> Users { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> BookSelector { get; set; } = new List<SelectListItem>();

        public List<BookRecord> Books { get; set; } = new List<BookRecord>();

        public CreateLibraryRecord(ILogger<CreateLibraryRecord> logger, IBookFacade bookFacade, ILibraryRecordFacade libraryRecordFacade)
        {
            this.logger = logger;
            this.bookFacade = bookFacade;
            this.libraryRecordFacade = libraryRecordFacade;
          
            BookSelector = bookFacade.GetBooksSelectorAsync().GetAwaiter().GetResult().Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Key.ToString(),
                                      Text = a.Value
                                  }).ToList();

            this.Books.Add(new BookRecord());
        }

        public IActionResult OnPostAddBook()
        {
            this.Books.Add(new BookRecord());
            return StatusCode(200);
        }

        public IActionResult OnPost(LibraryRecordCreateDTO LibraryRecordDto)
        {

            libraryRecordFacade.Create(LibraryRecordDto);

            logger.LogInformation("Book created");

            return RedirectToPage("/Books");
        }
    }

}
