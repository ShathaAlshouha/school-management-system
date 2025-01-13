using Microsoft.AspNetCore.Mvc;

namespace SchoolProject.Controllers
{
    public class LandingHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
