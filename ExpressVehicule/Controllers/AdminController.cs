using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExpressVoitures.Models.Entities;
using ExpressVoitures.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpressVoitures.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ExpressVoitures.Controllers
{
    public class AdminController : Controller
    {
        private readonly IVoitureService _VoitureService;
        private readonly ApplicationDbContext _context;

        public AdminController(IVoitureService voitureService, ApplicationDbContext context)
        {
            _VoitureService = voitureService;
            _context = context;
        }

        public IActionResult AddCar()
        {/*
            //ViewBag.Annees = new SelectList(_context.Vehicules.Select(v => v.Annee).Distinct().ToList());
            ViewBag.Marques = new SelectList(_context.Vehicules.Select(v => v.Marque).Distinct().ToList());
            ViewBag.Models = new SelectList(_context.Vehicules.Select(v => v.Model).Distinct().ToList());
            ViewBag.Finitions = new SelectList(_context.Vehicules.Select(v => v.Finition).Distinct().ToList());

            ViewBag.Reparations = new SelectList(_context.Reparations.Select(r => r.Type).Distinct().ToList());/**/

            return View();
        }


        //[Authorize]
        //[HttpPost]
        public async Task<IActionResult> CreateCar(DataAllInclusive model)
        {
            ModelState.Remove("Vehicule.TransactionAchat");

            if (model.Photo == null)
            {
                ModelState.Remove("Transaction.Photo");
                ModelState.Remove("DataAllInclusive.Transaction.Photo");
            }/**/

            if (ModelState.IsValid)
            {
                if (model.Photo != null && model.Photo.Length > 4)//2Mo
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.Photo.CopyToAsync(memoryStream);
                        model.Annonce.Photo = memoryStream.ToArray();
                    }
                }
                else
                {
                    ModelState.AddModelError("Photo", "La taille de l'image ne doit pas dépasser 2 Mo.");
                    return View("AddCar", model);
                }

                model.TransactionA.Type = TransactionType.Buy;
                model.TransactionA.VehiculeAchat = model.Vehicule;
                model.Vehicule.TransactionAchat = model.TransactionA;


                _VoitureService.SaveCar(model.Vehicule, [model.Reparation],model.TransactionA, model.Annonce, model.TransactionV);
                return RedirectToAction("Index", "Home");
            }
            return View("AddCar", model);
        }
        /*
        [HttpGet]
        public IActionResult GetModelsByMarque(string marque)
        {
            var models = _context.Vehicules.Where(v => v.Marque == marque).Select(v => v.Model).Distinct().ToList();
            return Json(models);
        }/**/

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
