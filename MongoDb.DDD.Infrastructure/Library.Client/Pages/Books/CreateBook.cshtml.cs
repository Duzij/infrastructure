using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.ApplicationLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Library.Client.Pages.Books
{
    public class CreateBook : PageModel
    {
        private readonly ILogger<CreateBook> logger;
        private readonly IBookFacade bookFacade;
        private readonly IAuthorFacade authorFacade;

        public BookCreateDTO BookDto { get; set; }

        public List<SelectListItem> Authors { get; set; } = new List<SelectListItem>();

        public CreateBook(ILogger<CreateBook> logger, IBookFacade bookFacade, IAuthorFacade authorFacade)
        {
            BookDto = new BookCreateDTO(Guid.NewGuid());
            this.logger = logger;
            this.bookFacade = bookFacade;
            this.authorFacade = authorFacade;
            Authors = authorFacade.GetAuthorSelectorAsync().GetAwaiter().GetResult().Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Key.ToString(),
                                      Text = a.Value
                                  }).ToList(); ;
        }

        public IActionResult OnPost()
        {
            BookDto.AuthorId = Request.Form["authorId"];
            BookDto.AuthorName = Authors.First(a => a.Value == Request.Form["authorId"]).Text;
            BookDto.Title = Request.Form["title"];
            BookDto.Description = Request.Form["description"];

            bookFacade.Create(BookDto);
            logger.LogInformation("Book created");

            return RedirectToPage("/Books");
        }
    }

}
