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
    public class AuthorsModel : PageModel
    {
        private readonly ILogger<AuthorsModel> _logger;
        private readonly IAuthorFacade authorFacade;

        public List<AuthorDetailDTO> Authors { get; set; } = new List<AuthorDetailDTO>();

        public AuthorsModel(ILogger<AuthorsModel> logger, IAuthorFacade authorFacade)
        {
            _logger = logger;
            this.authorFacade = authorFacade;
        }

        public async Task OnGet()
        {
            Authors = await authorFacade.GetAuthors();
        }
        public async Task OnPostDelete(Guid id)
        {
            await authorFacade.Delete(id.ToString());
            Authors = await authorFacade.GetAuthors();
        }
    }
}
