using Library.ApplicationLayer;
using Library.ApplicationLayer.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Client.Pages
{
    public class LibraryRecordsModel : PageModel
    {
        private readonly ILibraryRecordFacade libraryRecordFacade;

        public List<LibraryRecordDetailDTO> Records { get; set; }

        public LibraryRecordsModel(ILibraryRecordFacade libraryRecordFacade)
        {
            this.libraryRecordFacade = libraryRecordFacade;
        }

        public async Task OnGet()
        {
            Records = await libraryRecordFacade.GetAllLibraryRecords();
        }
    }
}
