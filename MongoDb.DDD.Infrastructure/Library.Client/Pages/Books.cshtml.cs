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
    public class BooksModel : PageModel
    {
        private readonly ILogger<BooksModel> _logger;
        private readonly IBookFacade bookFacade;
        public List<BookDetailDTO> Books { get; set; } = new List<BookDetailDTO>();

        public BooksModel(ILogger<BooksModel> logger, IBookFacade bookFacade)
        {
            _logger = logger;
            this.bookFacade = bookFacade;
        }

        public async Task OnGet()
        {
            Books = await bookFacade.GetBooks();
        }

        public async Task OnPostDelete(Guid id)
        {
            await bookFacade.Delete(id.ToString());
            Books = await bookFacade.GetBooks();
        }
    }
}
