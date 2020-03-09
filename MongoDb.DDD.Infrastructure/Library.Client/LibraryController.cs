using Library.ApplicationLayer;
using Library.ApplicationLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Client
{
    [Route("api")]
    public class LibraryController : Controller
    {
        private readonly IBookFacade bookFacade;
        private readonly ILogger<LibraryController> logger;

        public LibraryController(IBookFacade bookFacade, ILogger<LibraryController> logger)
        {
            this.bookFacade = bookFacade;
            this.logger = logger;
        }

        [HttpGet("books")]
        public JsonResult GetBooks()
        {
            return new JsonResult(bookFacade.GetBooksSelectorAsync().GetAwaiter().GetResult().Select(a =>
                                 new SelectListItem
                                 {
                                     Value = a.Key.ToString(),
                                     Text = a.Value
                                 }).ToList());
        }

        [HttpPost("bookLibrary")]
        public void AddBookLibrary(CreateLibraryForm form)
        {
            logger.LogInformation(form.ToString());
        }
    }

    public class CreateLibraryForm
    {
        public string userId { get; set; }
        public List<BookRecordDTO> books { get; set; }

    }

    public class BookRecordDTO
    {
        public string id { get; set; }
        public string amount { get; set; }
    }
}
