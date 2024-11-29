using Microsoft.AspNetCore.Mvc;

namespace Procademy_ASPNETCOREMVC_MVC.Controllers;

public class BookController : Controller
{
    // /Books?IsLogged=true&BookId=200
    [Route("/Books")]
    public IActionResult Books()
    {
        if (!Request.Query.ContainsKey("BookId"))
        {
            return BadRequest("Book Id is not provided!");
            return new BadRequestResult();
            Response.StatusCode = 400;
            return Content("Book Id is not provided!");
        }

        int bookId = Convert.ToInt32(Request.Query["BookId"]);
        if (bookId < 1 || bookId > 1000)
            return NotFound("Book Id is not in Range of 1 to 1000!");

        if (!Convert.ToBoolean(Request.Query["IsLogged"]))
        {
            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized);
            return Unauthorized("You are not logged in!");
            return new UnauthorizedObjectResult("");
            return new UnauthorizedResult();
        }

        return File("Sample/Machine_Learning_Operations_MLOps_Overview_Definition_and_Architecture.pdf", "application/pdf");
    }
}
