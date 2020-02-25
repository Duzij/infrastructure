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
    public class EditBook : PageModel
    {
        private readonly ILogger<EditBook> logger;
        private readonly IBookFacade bookFacade;

        [BindProperty]
        public BookDetailDTO BookDetail { get; set; }

        public List<SelectListItem> Authors { get; set; } = new List<SelectListItem>();


        public EditBook(ILogger<EditBook> logger, IBookFacade bookFacade, IAuthorFacade authorFacade)
        {
            this.logger = logger;
            this.bookFacade = bookFacade;
            Authors = authorFacade.GetAuthorSelectorAsync().GetAwaiter().GetResult().Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Key.ToString(),
                                      Text = a.Value
                                  }).ToList(); ;
        }

        public async Task OnGet()
        {
            BookDetail = await bookFacade.GetUserById(Request.Query.FirstOrDefault(a => a.Key == "id").Value);
        }

        public async Task<IActionResult> OnPost()
        {
            await bookFacade.Update(BookDetail);
            logger.LogInformation("User updated");

            return RedirectToPage("/Users");
        }

    }

}
