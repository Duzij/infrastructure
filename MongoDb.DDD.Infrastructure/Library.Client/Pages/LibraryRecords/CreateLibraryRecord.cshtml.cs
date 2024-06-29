using Library.ApplicationLayer;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Client.Pages.LibraryRecords
{
    public class CreateLibraryRecord : PageModel
    {
        public List<SelectListItem> Users { get; set; } = [];

        public CreateLibraryRecord(IUserFacade userFacade)
        {
            Users = userFacade.GetActiveUsersSelectorAsync().GetAwaiter().GetResult().Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Key.ToString(),
                                      Text = a.Value
                                  }).ToList();
        }
    }

}
