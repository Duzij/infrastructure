using Library.ApplicationLayer;
using Library.ApplicationLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILibraryRecordFacade libraryRecordFacade;

        public List<LibraryRecordDetailDTO> Records { get; set; }

        public IndexModel(ILibraryRecordFacade libraryRecordFacade)
        {
            this.libraryRecordFacade = libraryRecordFacade;
        }

        public async Task OnPostPay(string id)
        {
            await libraryRecordFacade.PayLibraryRecord(id);
            Records = await libraryRecordFacade.GetLibraryRecords();
        }

        public async Task OnGet()
        {
            Records = await libraryRecordFacade.GetLibraryRecords();
        }
    }
}
