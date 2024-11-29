using System.ComponentModel.DataAnnotations;

namespace Procademy_ASPNETCOREMVC_MVC.Models;

public class BooksValidation
{
    public int? BookID { get; set; }
    [Required] // 반드시 제공 되어야 한다.
    public string Name { get; set; }
    [Required(ErrorMessage = "Author is a required field")]
    public string Author { get; set; }
}