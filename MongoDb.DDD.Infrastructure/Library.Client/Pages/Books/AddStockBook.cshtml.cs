using Library.ApplicationLayer;
using Library.ApplicationLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Client.Pages.Books
{
    public class AddStockBook : PageModel
    {
        private readonly ILogger<AddStockBook> logger;
        private readonly IBookFacade bookFacade;

        [BindProperty]
        public BookDetailDTO BookDetail { get; set; }

        public int AmountValue { get; set; }

        public AddStockBook(ILogger<AddStockBook> logger, IBookFacade bookFacade)
        {
            this.logger = logger;
            this.bookFacade = bookFacade;
        }

        public async Task OnGet()
        {
            BookDetail = await bookFacade.GetUserById(Request.Query.FirstOrDefault(a => a.Key == "id").Value);
        }

        public async Task<IActionResult> OnPostAddAmount(int amountValue)
        {
            await bookFacade.UpdateAmount(BookDetail.Id, amountValue);
            logger.LogInformation("Book amount updated");

            return RedirectToPage("/Books");
        }

    }

}
