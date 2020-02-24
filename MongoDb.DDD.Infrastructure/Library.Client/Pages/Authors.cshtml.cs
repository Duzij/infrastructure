using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Library.Client.Pages
{
    public class AuthorsModel : PageModel
    {
        private readonly ILogger<AuthorsModel> _logger;

        public AuthorsModel(ILogger<AuthorsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
