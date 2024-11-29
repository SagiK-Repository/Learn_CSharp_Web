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

        [Route("/category/note")]
        public IActionResult CategoryNote() => Content("Note Page", "text/plain");
    }
}
