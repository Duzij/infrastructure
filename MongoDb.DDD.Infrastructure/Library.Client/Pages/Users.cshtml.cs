using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.ApplicationLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Library.Client.Pages
{
    public class UsersModel : PageModel
    {
        private readonly ILogger<UsersModel> _logger;
        private readonly IUserFacade userFacade;

        public List<UserDetailDTO> Users { get; set; } = new List<UserDetailDTO>();

        public UsersModel(ILogger<UsersModel> logger, IUserFacade userFacade)
        {
            _logger = logger;
            this.userFacade = userFacade;
        }

        public async Task OnGet()
        {
            Users = await userFacade.GetUsers();
        }

        public async Task OnPostDelete(Guid id)
        {
            await userFacade.Delete(id.ToString());
            Users = await userFacade.GetUsers();
        }
    }
}
