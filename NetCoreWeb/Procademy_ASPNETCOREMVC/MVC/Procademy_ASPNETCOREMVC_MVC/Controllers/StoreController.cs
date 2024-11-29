using Microsoft.AspNetCore.Mvc;

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

            return Content($"Logged = {isLogged}, NoteId = {noteId}", "text/plain");
        }
    }
}
