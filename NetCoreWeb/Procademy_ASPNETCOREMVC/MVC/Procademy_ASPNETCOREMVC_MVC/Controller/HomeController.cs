﻿using Microsoft.AspNetCore.Mvc;

namespace Procademy_ASPNETCOREMVC_MVC.Controller;

public class HomeController
{
    [Route("/")]
    [Route("Home")]
    public string Index() => "Welcome ASP.NET Core Application!";

    [Route("ContentReult")]
    public ContentResult ContentResult() => new ContentResult()
    {
        Content= "Welcome ASP.NET Core Application!",
        ContentType= "text/plain"
    };

    [Route("ContentReult_Html")]
    public ContentResult HtmlResult() => new ContentResult()
    {
        Content = "<h1>Welcome ASP.NET Core Application!<h1>",
        ContentType = "text/html"
    };

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

public class ContentResultController : Microsoft.AspNetCore.Mvc.Controller
{
    [Route("ContentResult_Content")]
    public ContentResult ContentResult_Content() => Content("<h1>You are in About Page!<h1>", "text/html");
}