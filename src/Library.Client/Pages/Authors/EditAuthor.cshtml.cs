using Library.ApplicationLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Client.Pages.Authors
{
    public class EditAuthor : PageModel
    {
        private readonly ILogger<EditAuthor> logger;
        private readonly IAuthorFacade authorFacade;

        [BindProperty]
        public AuthorDetailDTO AuthorDto { get; set; }

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
            AuthorDto.Name = Request.Form["name"];
            AuthorDto.Surname = Request.Form["surname"];

            await authorFacade.Update(AuthorDto);
            logger.LogInformation("Author updated");
            return RedirectToPage("/Authors");
        }

    }

}
