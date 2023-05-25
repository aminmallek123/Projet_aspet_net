using Microsoft.AspNetCore.Mvc;
using Projet_aspet.data;
using Projet_aspet.Models;
namespace Projet_aspet.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
