using ExpressVoitures.Models;
using ExpressVoitures.Models.Entities;
using ExpressVoitures.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExpressVoitures.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVoitureService _VoitureService;

        public HomeController(ILogger<HomeController> logger, IVoitureService voitureService)
        {
            _logger = logger;
            _VoitureService = voitureService;
        }

        public IActionResult Index()
        {
            IEnumerable<Transaction> transactions = _VoitureService.GetAllTransactions();
            return View(transactions);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
