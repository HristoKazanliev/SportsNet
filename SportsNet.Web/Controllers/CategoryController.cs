namespace SportsNet.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CategoryController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
