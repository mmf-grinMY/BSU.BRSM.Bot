using Microsoft.AspNetCore.Mvc;

namespace BSU.BRSM.Bot
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
