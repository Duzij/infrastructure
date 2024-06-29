using Library.ApplicationLayer;
using Library.ApplicationLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Client.Pages.Users
{
    public class CreateUser : PageModel
    {
        private readonly ILogger<CreateUser> logger;
        private readonly IUserFacade userFacade;

        public CreateUserDTO UserDto { get; set; }

        public CreateUser(ILogger<CreateUser> logger, IUserFacade userFacade)
        {
            UserDto = new CreateUserDTO(Guid.NewGuid());
            this.logger = logger;
            this.userFacade = userFacade;
        }

        public IActionResult OnPost()
        {
            UserDto.Email = Request.Form["emailaddress"];
            UserDto.Name = Request.Form["name"];
            UserDto.Surname = Request.Form["surname"];

            userFacade.Create(UserDto);
            logger.LogInformation("User created");

            return RedirectToPage("/Users");
        }
    }

}
