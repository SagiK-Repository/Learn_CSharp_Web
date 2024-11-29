using Microsoft.AspNetCore.Mvc;

namespace Procademy_ASPNETCOREMVC_MVC.Controllers;

public class FileController : Controller
{
    [Route("File/Download-file")]
    public VirtualFileResult Index()
    {
        return new ("Sample/Machine_Learning_Operations_MLOps_Overview_Definition_and_Architecture.pdf","application/pdf");
        return File("Sample/Machine_Learning_Operations_MLOps_Overview_Definition_and_Architecture.pdf", "application/pdf"); // 동일한 결과
    }

    [Route("File/Pysical/Download-file")]
    public PhysicalFileResult PysicalFile()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "wwwroot", "Sample", "Machine_Learning_Operations_MLOps_Overview_Definition_and_Architecture.pdf");
        return new(path, "application/pdf");
        return PhysicalFile(path, "application/pdf"); // 동일한 결과
    }

    [Route("File/Byte/Download-file")]
    public FileContentResult ByteFile()
    {
        byte[] byteData = System.IO.File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "wwwroot", "Sample", "Machine_Learning_Operations_MLOps_Overview_Definition_and_Architecture.pdf"));
        return new(byteData, "application/pdf");
        return File(byteData, "application/pdf"); // 동일한 결과
   }

    [Route("ActionResult")]
    public IActionResult FileActionResult()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "wwwroot", "Sample", "Machine_Learning_Operations_MLOps_Overview_Definition_and_Architecture.pdf");

        if (System.IO.File.Exists(path))
            return PhysicalFile(path, "application/pdf");

        return Content("File you specified is not found!", "text/plain");
    }
}