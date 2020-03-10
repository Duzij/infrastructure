using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.ApplicationLayer;
using Library.ApplicationLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Library.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ILibraryRecordFacade libraryRecordFacade;

        public List<LibraryRecordDetailDTO> Records { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ILibraryRecordFacade libraryRecordFacade)
        {
            _logger = logger;
            this.libraryRecordFacade = libraryRecordFacade;
        }

        public async Task OnGet()
        {
            Records = await libraryRecordFacade.GetLibraryRecords();
        }
    }
}
