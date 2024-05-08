using Microsoft.AspNetCore.Mvc;

namespace BloodBank.Controllers
{
    public class Hospitals : Controller
    {
        public IActionResult Hospital()
        {
            return View();
        }
    }
}
