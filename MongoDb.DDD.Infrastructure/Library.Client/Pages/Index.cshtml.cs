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
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBookFacade bookFacade;

        public IndexModel(ILogger<IndexModel> logger, IBookFacade bookFacade)
        {
            _logger = logger;
            this.bookFacade = bookFacade;
        }

        public void OnGet()
        {
            
        }
    }
}
