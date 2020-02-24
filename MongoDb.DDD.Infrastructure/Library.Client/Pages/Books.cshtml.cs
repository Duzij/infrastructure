using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Library.Client.Pages
{
    public class BooksModel : PageModel
    {
        private readonly ILogger<BooksModel> _logger;

        public BooksModel(ILogger<BooksModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
