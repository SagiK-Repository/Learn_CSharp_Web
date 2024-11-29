var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); // Controller 접미사를 탐색하여 등록 - 예) HomeController
var app = builder.Build();

//app.UseRouting();
//app.UseEndpoints(endpoints =>
//{
//    app.MapControllers(); // 모든 컨트롤러와 작업 메서드를 선택 후 Route 자동추가
//});
app.MapControllers(); // 위 주석 대신에 사용 가능
// 자동으로 UseRouting() 호출 및 배후에서 EndPoint를 사용

app.Run();