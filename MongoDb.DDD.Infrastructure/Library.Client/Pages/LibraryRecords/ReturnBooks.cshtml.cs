using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.ApplicationLayer;
using Library.ApplicationLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Library.Client.Pages.LibraryRecords
{
    public class ReturnBooks : PageModel
    {
        private readonly ILogger<ReturnBooks> logger;
        private readonly ILibraryRecordFacade libraryRecordFacade;

        [BindProperty]
        public LibraryRecordDetailDTO libraryRecordDetail { get; set; }

        public ReturnBooks(ILogger<ReturnBooks> logger, ILibraryRecordFacade libraryRecordFacade)
        {
            this.logger = logger;
            this.libraryRecordFacade = libraryRecordFacade;
        }

        public async Task OnGet()
        {
            libraryRecordDetail = await libraryRecordFacade.GetLibraryRecordById(Request.Query.FirstOrDefault(a => a.Key == "id").Value.ToString());
        }

    }

}
