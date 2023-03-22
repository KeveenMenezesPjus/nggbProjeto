using Microsoft.AspNetCore.Mvc;

namespace nggbProjeto.Controllers
{
    public class FeedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
