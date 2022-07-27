using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Entities;
using Data.Repositories;
using Data.Mapper;
using Data.Models;

namespace Protium.Web.Controllers
{
    public class DriversController : Controller
    {
        private readonly IRepository<Driver> driverRepo;
        public DriversController(IRepository<Driver> _driverRepo)
        {
            
            driverRepo = _driverRepo;
        }

        // GET: Drivers
        public async Task<IActionResult> Index()
        {
            var drivers = await driverRepo.GetAllReadOnly();
            return View(drivers.Select(x => x.Map()));
        }

        // GET: Drivers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var driver = await driverRepo.GetById(id);
            if (driver == null)
                return NotFound();

            return View(driver.Map());
        }

        // GET: Drivers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Drivers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDriverRequest driver)
        {
            if (ModelState.IsValid)
            {
                driver.Validate();
                var shipmentResult = await driverRepo.Add(driver.Map());
                return RedirectToAction(nameof(Index));
            }
            
            return View(driver);
        }

        // GET: Drivers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var driver = await driverRepo.GetById(id);
            if (driver == null)
            {
                return NotFound();
            }
            
            return View(driver.MapForUpdate());
        }

        // POST: Drivers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UpdateDriverRequest model)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    model.Validate();
                    var updatedDriver = await driverRepo.Update(model.Map());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await DriverExists(model.id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            } 
            return View(model);
        }

        // GET: Drivers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return NotFound();

                var deletedShipment = await driverRepo.Remove(id);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        private async Task<bool> DriverExists(string id)
        {
            return ((await driverRepo.GetAllReadOnly()).Any(e => e.id == id));
        }
    }
}
