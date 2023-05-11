using Microsoft.AspNetCore.Mvc;

namespace Book_API.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
