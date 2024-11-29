using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Procademy_ASPNETCOREMVC_MVC.Models;

namespace Procademy_ASPNETCOREMVC_MVC.Controllers
{
    public class StoreController : Controller
    {
        [Route("/Note")]
        public IActionResult Note()
        {
            return RedirectToAction("CategoryNote", "Store", new { });
            return new RedirectToActionResult("CategoryNote", "Store", new { });

            return RedirectToActionPermanent("CategoryNote", "Store", new { }); // 페이지가 영구적으로 이동되었음을 나타내는 인수 - 301 Code
            return new RedirectToActionResult("CategoryNote", "Store", new { }, permanent: true);
        }

        [Route("/LocalRedirect")]
        public IActionResult LocalRedirect()
        {
            return new LocalRedirectResult("/category/note", permanent: true);
        }

        [Route("/category/note")]
        public IActionResult CategoryNote() => Content("Note Page", "text/plain");
    }

    public class NoteController : Controller
    {
        // /Note?IsLogged=true&NoteId=200
        [Route("/Notes")]
        public IActionResult Notes()
        {
            int noteId = Convert.ToInt32(Request.Query["BookId"]);
            bool isLogged = Convert.ToBoolean(Request.Query["IsLogged"]);
            return new RedirectResult($"/category/note/{noteId}/{isLogged}");
            return new RedirectResult("https://google.com");
            return LocalRedirectPermanent($"/category/note/{noteId}/{isLogged}");
        }

        // /category/note/?IsLogged=true&NoteId=200
        [Route("/category/note/{noteid}/{isLogged}")]
        public IActionResult NoteCategory()
        {
            if (!Request.RouteValues.ContainsKey("noteid"))
                return BadRequest("Book Id is not provided!");

            int noteId = Convert.ToInt32(Request.Query["BookId"]);
            if (noteId < 1 || noteId > 1000)
                return NotFound("Book Id is not in Range of 1 to 1000!");

            bool isLogged = Convert.ToBoolean(Request.Query["IsLogged"]);
            if (!isLogged)
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized);

            return Content($"Logged = {isLogged}, Note ID = {noteId}", "text/plain");
        }
    }

    public class ParameterBinding : Controller
    {
        // /Parameter?IsLogged=true&NoteId=200
        [Route("/Paremeter")]
        public IActionResult Parameter(int parameterId, bool isLogged)
        {
            return Content($"Logged = {isLogged}, Parameter ID = {parameterId}", "text/plain");
        }

        // /Parameter/100/false/?IsLogged=true&NoteId=200 => 100, false가 우선순위
        [Route("/Paremeter/{parameterid}/{islogged}")]
        public IActionResult ParameterDepth(int? parameterId, bool isLogged)
        {
            return Content($"Logged = {isLogged}, Parameter ID = {parameterId}", "text/plain");
        }

        // /Parameter/false => Paremeter ID not provided
        [Route("/Paremeters/{parameterId?}/{islogged?}")]
        public IActionResult ParameterFromQuery([FromRoute]int? parameterId, [FromQuery]bool isLogged)
        {
            if (parameterId.HasValue == false)
                return Content("Paremeter ID not provided", "text/plain");
            return Content($"Logged = {isLogged}, Parameter ID = {parameterId}", "text/plain");
        }

        [Route("/BookBinidng/{BookID?}/{Author?}")]
        public IActionResult BookBinding(Books book)
        {
            if (book.BookID.HasValue == false)
                return Content("Book ID not provided", "text/plain");
            return Content($"Book ID = {book.BookID}, Author = {book.Author}", "text/plain");
        }

        [Route("/BookBinidngFromQuery/{BookID?}/{Author?}")]
        public IActionResult BookBindingFromQuery([FromQuery] Books book)
        {
            if (book.BookID.HasValue == false)
                return Content("Book ID not provided", "text/plain");
            return Content($"Book ID = {book.BookID}, Author = {book.Author}", "text/plain");
        }

        [Route("/BookBinidngFromModel/{BookID?}/{Author?}")]
        public IActionResult BookBindingFromModel(BooksChoose book)
        {
            if (book.BookID.HasValue == false)
                return Content("Book ID not provided", "text/plain");
            return Content($"Book ID = {book.BookID}, Author = {book.Author}", "text/plain");
        }

        [Route("/Validation/{BookID?}/{Author?}")]
        public IActionResult BookBindingFromModel(BooksValidation book)
        {
            if (book.BookID.HasValue == false)
                return Content("Book ID not provided", "text/plain");

            if (!ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                foreach(var value in ModelState.Values)
                    foreach(var error in value.Errors)
                        errors.Add(error.ErrorMessage);

                return BadRequest(string.Join("\n", errors));
                return BadRequest("Some field values are not valid");
            }

            return Content($"Book ID = {book.BookID}, Author = {book.Author}", "text/plain");
        }
    }
}
