﻿using ExpressVoitures.Models.Entities;
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


        public IActionResult CarIndex(Vehicule voiture)
        {
            return View(voiture);
        }

        public IActionResult DeletCar(int id)
        {
            _VoitureService.DeleteCar(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public RedirectToActionResult SeeCarIndex(int id)
        {
            Vehicule voiture = _VoitureService.GetCarById(id);

            if (voiture != null)
            {
                return RedirectToAction("CarIndex", voiture);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
