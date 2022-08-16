using Microsoft.AspNetCore.Mvc;

namespace Jobby.Client.Controllers;
public class AuthController : Controller
{
    public IActionResult Login()
    {
        return View();
    }
}
