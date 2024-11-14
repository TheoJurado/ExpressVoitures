using ExpressVoitures.Data;
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
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IVoitureService voitureService, ApplicationDbContext context)
        {
            _logger = logger;
            _VoitureService = voitureService;
            _context = context;
        }


        public IActionResult Index()
        {
            var annonces = _context.Annonces.ToList();
            foreach (var annonce in annonces)
            {
                annonce.Vehicule = _VoitureService.GetCarById(annonce.VehiculeId);
            }
            return View(annonces);
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
