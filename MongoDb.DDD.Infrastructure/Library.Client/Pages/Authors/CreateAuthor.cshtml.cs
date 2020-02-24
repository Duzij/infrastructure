﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.ApplicationLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Library.Client.Pages.Authors
{
    public class CreateAuthor : PageModel
    {
        private readonly ILogger<CreateAuthor> logger;
        private readonly IAuthorFacade authorFacade;

        public CreateAuthorDTO AuthorDto { get; set; }

        public CreateAuthor(ILogger<CreateAuthor> logger, IAuthorFacade authorFacade)
        {
            AuthorDto = new CreateAuthorDTO(Guid.NewGuid());
            this.logger = logger;
            this.authorFacade = authorFacade;
        }

        public IActionResult OnPost()
        {
            AuthorDto.Name = Request.Form["name"];
            AuthorDto.Surname = Request.Form["surname"];

            authorFacade.Create(AuthorDto);
            logger.LogInformation("Author created");

            return RedirectToPage("/Authors");
        }
    }

}
