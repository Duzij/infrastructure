using Library.ApplicationLayer;
using Library.ApplicationLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Client.Pages.Books
{
    public class EditBook : PageModel
    {
        private readonly ILogger<EditBook> logger;
        private readonly IBookFacade bookFacade;
        private readonly IAuthorFacade authorFacade;

        [BindProperty]
        public BookDetailDTO BookDetail { get; set; }

        public List<SelectListItem> Authors { get; set; } = [];

        public EditBook(ILogger<EditBook> logger, IBookFacade bookFacade, IAuthorFacade authorFacade)
        {
            this.logger = logger;
            this.bookFacade = bookFacade;
            this.authorFacade = authorFacade;
        }

        public async Task OnGet()
        {
            BookDetail = await bookFacade.GetUserById(Request.Query.FirstOrDefault(a => a.Key == "id").Value);

            Authors = authorFacade.GetAuthorSelectorAsync().GetAwaiter().GetResult().Select(a =>
                                  new SelectListItem
                                  {
                                      Selected = BookDetail.AuthorId == a.Key,
                                      Value = a.Key.ToString(),
                                      Text = a.Value
                                  }).ToList();
        }

        public async Task<IActionResult> OnPost()
        {
            await bookFacade.Update(BookDetail);
            logger.LogInformation("Book updated");

            return RedirectToPage("/Books");
        }

    }

}
