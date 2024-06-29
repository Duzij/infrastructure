using Library.ApplicationLayer;
using Library.ApplicationLayer.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Client.Pages
{
    public class AuthorsModel : PageModel
    {
        private readonly ILogger<AuthorsModel> _logger;
        private readonly IAuthorFacade authorFacade;

        public List<AuthorDetailDTO> Authors { get; set; } = [];

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
