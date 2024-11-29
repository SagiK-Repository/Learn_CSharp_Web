using Microsoft.AspNetCore.Mvc;

namespace Procademy_ASPNETCOREMVC_MVC.Controller;

public class HomeController
{
    [Route("/")]
    [Route("Home")]
    public string Index() => "Welcome ASP.NET Core Application!";

    [Route("About")]
    public string About() => "You are in About Page!";

    [Route("Contact-Us")]
    public string Contact() => "You are in Contact Page!";
}

[Controller] // 접미사 안쓰는 경우 다음과 같이 Attribute로 정의
public class Home
{
    [Route("/products/{id:int:min(1000):max(9999)}")]
    public string Products(int id) => $"You are in Products({id}) Page!";
}