using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

public class BookValidation
{
    public int? BookID { get; set; }
    [Required(ErrorMessage = "{0} is a required field")]
    [Display(Name = "Book Name")]
    [StringLength(10, MinimumLength = 4, ErrorMessage = "(0} must have chacters between {3} and {1}")]
    [RegularExpression("*[a-zA-Z ]$", ErrorMessage = "Book name is not valid")]
    public string BookName { get; set; }
    [ValidateNever]
    public string Author { get; set; }
    [Range(0, 999.99, ErrorMessage = "{0} must be between (1} and (2)")]
    public Decimal Price { get; set; }
    [EmailAddress(ErrorMessage = "Email format is not valid")]
    public string AuthorEmail { get; set; }
    public string Password { get; set; }
    [Compare("Password", ErrorMessage = "Password does not match ConfirmPassword")]
    public string ConfirmPassword { get; set; }
}