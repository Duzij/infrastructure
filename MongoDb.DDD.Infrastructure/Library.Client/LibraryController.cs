using Library.ApplicationLayer;
using Library.ApplicationLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        private readonly ILibraryRecordFacade libraryRecordFacade;
        private readonly ILogger<LibraryController> logger;

        public LibraryController(IBookFacade bookFacade, ILibraryRecordFacade libraryRecordFacade, ILogger<LibraryController> logger)
        {
            this.bookFacade = bookFacade;
            this.libraryRecordFacade = libraryRecordFacade;
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
        public IActionResult AddBookLibrary([FromBody] LibraryRecordCreateDTO form)
        {
            libraryRecordFacade.Create(form);
            return Ok();
        }
    }


}
