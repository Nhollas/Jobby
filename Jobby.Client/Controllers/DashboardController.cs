using Microsoft.AspNetCore.Mvc;

namespace Jobby.Client.Controllers;
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
