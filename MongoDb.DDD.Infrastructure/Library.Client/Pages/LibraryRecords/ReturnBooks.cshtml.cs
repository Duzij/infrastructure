using Library.ApplicationLayer;
using Library.ApplicationLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
