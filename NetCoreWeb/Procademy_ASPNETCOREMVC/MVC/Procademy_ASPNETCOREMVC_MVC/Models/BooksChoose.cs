using Microsoft.AspNetCore.Mvc;

namespace Procademy_ASPNETCOREMVC_MVC.Models;

public class BooksChoose
{
    [FromRoute]
    public int? BookID { get; set; }
    [FromQuery]
    public string Author { get; set; }
}