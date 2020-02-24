using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.ApplicationLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Library.Client.Pages.Authors
{
    public class EditAuthor : PageModel
    {
        private readonly ILogger<EditAuthor> logger;
        private readonly IAuthorFacade authorFacade;

        [BindProperty]
        public AuthorDetailDTO AuthorDto { get; set; }

        public bool IsBanned { get; set; }

        public EditAuthor(ILogger<EditAuthor> logger, IAuthorFacade authorFacade)
        {
            this.logger = logger;
            this.authorFacade = authorFacade;
        }

        public async Task OnGet()
        {
            AuthorDto = await authorFacade.GetById(Request.Query.FirstOrDefault(a => a.Key == "id").Value);
        }

        public async Task<IActionResult> OnPost()
        {
            await authorFacade.Update(AuthorDto);
            logger.LogInformation("Author updated");
            return RedirectToPage("/Authors");
        }

    }

}
