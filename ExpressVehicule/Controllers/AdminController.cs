using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExpressVoitures.Models.Entities;
using ExpressVoitures.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpressVoitures.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ExpressVoitures.Models.ViewModels;
using ExpressVoitures.Models;
using Microsoft.Extensions.Options;

namespace ExpressVoitures.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    public class AdminController : Controller
    {
        private readonly IVoitureService _VoitureService;
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

        public AdminController(IVoitureService voitureService, ApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            _VoitureService = voitureService;
            _context = context;
            _appSettings = appSettings.Value;
        }

        public IActionResult AddCar()
        {

            return View();
        }


        //[Authorize]
        //[HttpPost]
        public async Task<IActionResult> CreateCar(DataAllInclusive model)
        {
            //ModelState.Remove("Vehicule.TransactionAchat");
            ModelState.Remove("dataVehicule.TransactionAchat");

            if (model.Photo == null)
            {
                ModelState.Remove("Transaction.Photo");
                ModelState.Remove("DataAllInclusive.Transaction.Photo");
            }/**/

            model.TransactionA.Type = TransactionType.Buy;
            model.TransactionA.VehiculeLinked = model.dataVehicule;
            model.dataVehicule.TransactionAchat = model.TransactionA;
            

            if (ModelState.IsValid)
            {
                if (model.Photo != null && model.Photo.Length > 4)//2Mo
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.Photo.CopyToAsync(memoryStream);
                        model.dataAnnonce.Photo = memoryStream.ToArray();
                    }
                }
                else
                {
                    ModelState.AddModelError("Photo", "La taille de l'image ne doit pas dépasser 2 Mo.");
                    return View("AddCar", model);
                }



                _VoitureService.SaveCar(model.dataVehicule, model.dataReparations,model.TransactionA, model.dataAnnonce, model.TransactionV);
                return RedirectToAction("Index", "Home");
            }
            return View("AddCar", model);
        }

        public IActionResult UpdateCar(int id)
        {
            var annonce = _VoitureService.GetAnnonceById(id);
            if(annonce == null)
                return NotFound();

            if (annonce.Vehicule == null)
                throw new NullReferenceException("Le véhicule associé à l'annonce est null.");
            if (annonce.Vehicule.TransactionAchat == null)
                throw new NullReferenceException("La transaction d'achat associée au véhicule est null.");

            var allData = new DataAllInclusive
            {
                dataAnnonce = annonce,
                dataVehicule = annonce.Vehicule,
                TransactionA = annonce.Vehicule.TransactionAchat ?? new Transaction(),
                TransactionV = annonce.Vehicule.TransactionVente ?? new Transaction(),
                dataReparations = annonce.Vehicule.Reparations?.ToList() ?? new List<Reparation>(),
                Photo = annonce.Photo != null
                    ? VoitureService.ByteArrayToFormFile(annonce.Photo, "photo.jpg", "image/jpeg")
                    : null
            };/**/
            
            return View(allData);
        }

        [HttpPost]
        public IActionResult SaveUpdateCar([FromForm]DataAllInclusive model)
        {
            double price = model.TransactionA.Price + _appSettings.Marge;
            foreach (Reparation rep in model.dataReparations)
                price += rep.Prix;

            if (!_VoitureService.UpdateAnnonce(model.dataAnnonce.Id, model.dataAnnonce, price))
                return BadRequest("Erreur lors de la mise à jour de l'annonce.");
            if (!_VoitureService.UpdateVehicule(model.dataVehicule.Id, model.dataVehicule, model.isAdministrator))
                return BadRequest("Erreur lors de la mise à jour du véhicule.");
            if (!_VoitureService.UpdateReparations(model.dataVehicule.Id, model.dataReparations))
                return BadRequest("Erreur lors de la mise à jour des réparations.");
            /**/

            return RedirectToAction("Index", "Home");
        }

        




        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
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

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
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

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
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
