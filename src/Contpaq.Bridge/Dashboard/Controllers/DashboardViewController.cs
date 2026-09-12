using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Contpaq.Bridge.Dashboard.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DashboardViewController : Controller
    {
        [HttpGet("/")]
        [HttpGet("/dashboard")]
        public IActionResult Index()
        {
            var baseDir = AppContext.BaseDirectory;
            var htmlPath = Path.Combine(baseDir, "Dashboard", "wwwroot", "index.html");
            if (!System.IO.File.Exists(htmlPath))
            {
                htmlPath = Path.Combine(Directory.GetCurrentDirectory(), "Dashboard", "wwwroot", "index.html");
            }

            if (System.IO.File.Exists(htmlPath))
            {
                return PhysicalFile(htmlPath, "text/html");
            }
            return Content("<h1>CONTPAQi Integration Bridge Dashboard</h1><p>index.html missing</p>", "text/html");
        }
    }
}
