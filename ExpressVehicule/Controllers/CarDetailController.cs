using ExpressVoitures.Models.Entities;
using ExpressVoitures.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpressVoitures.Controllers
{
    public class CarDetailController : Controller
    {

        private readonly IVoitureService _VoitureService;


        public CarDetailController(IVoitureService voitureService)
        {
            _VoitureService = voitureService;
        }


        public IActionResult CarIndex(int idAnnonce)
        {
            var annonce = _VoitureService.GetAnnonceById(idAnnonce);
            if (annonce == null)
            {
                return RedirectToAction("Index", "Home");
            }
            annonce.Vehicule = _VoitureService.GetCarById(annonce.VehiculeId);
            return View(annonce);
        }

        public IActionResult DeleteCar(int id)
        {
            _VoitureService.DeleteAnnonce(id);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult UpdateCar(int id)
        {
            return RedirectToAction("UpdateCar", "Admin", new { id = id });
        }

        [HttpPost]
        public RedirectToActionResult SeeCarIndex(int idToTransfer)
        {
            return RedirectToAction("CarIndex", new { idAnnonce = idToTransfer });
        }


        // GET: CarDetailController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CarDetailController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarDetailController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarDetailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarDetailController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CarDetailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarDetailController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CarDetailController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
