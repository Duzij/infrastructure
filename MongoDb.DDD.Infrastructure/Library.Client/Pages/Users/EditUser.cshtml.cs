﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.ApplicationLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Library.Client.Pages.Users
{
    public class EditUser : PageModel
    {
        private readonly ILogger<EditUser> logger;
        private readonly IUserFacade userFacade;

        [BindProperty]
        public UserDetailDTO UserDto { get; set; }

        public bool IsBanned { get; set; }

        public EditUser(ILogger<EditUser> logger, IUserFacade userFacade)
        {
            this.logger = logger;
            this.userFacade = userFacade;
        }

        public async Task OnGet()
        {
            UserDto = await userFacade.GetUserById(Request.Query.FirstOrDefault(a => a.Key == "id").Value);
            IsBanned = UserDto.IsBanned;
        }

        public async Task<IActionResult> OnPost()
        {
            await userFacade.Update(UserDto);
            logger.LogInformation("User updated");

            return RedirectToPage("/Users");
        }

    }

}
