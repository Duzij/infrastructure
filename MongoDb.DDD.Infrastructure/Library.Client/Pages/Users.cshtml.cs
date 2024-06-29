using Library.ApplicationLayer;
using Library.ApplicationLayer.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Client.Pages
{
    public class UsersModel : PageModel
    {
        private readonly IUserFacade userFacade;

        public List<UserDetailDTO> Users { get; set; } = [];

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
