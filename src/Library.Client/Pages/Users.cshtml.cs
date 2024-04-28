using Library.ApplicationLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Client.Pages
{
    public class UsersModel : PageModel
    {
        private readonly IUserFacade userFacade;

        public List<UserDetailDTO> Users { get; set; } = new List<UserDetailDTO>();

        public UsersModel(IUserFacade userFacade)
        {
            this.userFacade = userFacade;
        }

        public async Task OnGet()
        {
            Users = await userFacade.GetUsers();
        }
    }
}
