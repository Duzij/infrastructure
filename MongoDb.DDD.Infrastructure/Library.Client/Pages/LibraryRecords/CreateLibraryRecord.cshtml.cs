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
    public class CreateLibraryRecord : PageModel
    {
        public List<SelectListItem> Users { get; set; } = new List<SelectListItem>();

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
