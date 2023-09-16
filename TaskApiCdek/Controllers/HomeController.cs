using Microsoft.AspNetCore.Mvc;
using TaskApiCdek.Repositories;
using TaskApiCdek.ViewModels;

namespace TaskApiCdek.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICdekClient _client;
        public HomeController(ICdekClient client)
        {
            _client = client;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(new IndexViewModel());
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            var response = _client.CalculateTariff(model);
            ViewData["cost"] = response;
            return View(model);
        }
    }
}
