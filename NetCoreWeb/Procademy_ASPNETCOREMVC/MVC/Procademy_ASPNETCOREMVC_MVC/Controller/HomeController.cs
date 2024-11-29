using Microsoft.AspNetCore.Mvc;

namespace Procademy_ASPNETCOREMVC_MVC.Controller;

public class HomeController
{
    [Route("Home")]
    public string Index() => "Welcome ASP.NET Core Application!";
}