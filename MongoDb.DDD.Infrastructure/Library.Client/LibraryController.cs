using Library.ApplicationLayer;
using Library.ApplicationLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

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


        [HttpPost("addBookLibrary")]
        public async Task<IActionResult> AddBookLibrary([FromBody] LibraryRecordCreateDTO form)
        {
            try
            {
                await libraryRecordFacade.Create(form);
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new { responseText = "Your form successfuly sent!" });
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { responseText = ex.Message });
            }
        }

        [HttpGet("booksToReturn")]
        public JsonResult GetBooksToReturn(string id)
        {
            return new JsonResult(libraryRecordFacade.GetLibraryRecordById(id).GetAwaiter().GetResult().Books);
        }

        [HttpPost("returnBook")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookDTO form)
        {
            try
            {
                await libraryRecordFacade.ReturnBookAsync(form);
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new { responseText = "Your form successfuly sent!" });
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { responseText = ex.Message });
            }
        }


    }


}
