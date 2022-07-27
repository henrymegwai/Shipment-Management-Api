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
using Data.Helper;

namespace Protium.Web.Controllers
{
    public class ShipmentsController : Controller
    {
        private readonly IRepository<Shipment> shipmentRepo;
        private readonly IRepository<Driver> driverRepo;
        public ShipmentsController(IRepository<Shipment> _shipmentRepo, 
            IRepository<Driver> _driverRepo)
        {
            shipmentRepo = _shipmentRepo;
            driverRepo = _driverRepo;
        }

        // GET: Shipments
        public async Task<IActionResult> Index()
        {
            var shipments = await shipmentRepo.GetAllReadOnly();
            return View(shipments.Select(x => x.Map()));
        }

        // GET: Shipments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();
          
            var shipment = await shipmentRepo.GetById(id);
            if (shipment == null)
              return NotFound();
            
            return View(shipment.Map());
        }

        // GET: Shipments/Create
        public async Task<IActionResult> Create()
        {
            ViewData["driverid"] = new SelectList(await driverRepo.GetForDropDown(), "id", "name");
            return View();
        }

        // POST: Shipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateShipmentRequest shipment)
        {
            if (ModelState.IsValid)
            {
                shipment.Validate();
                var shipmentResult = await shipmentRepo.Add(shipment.Map());
                return RedirectToAction(nameof(Index));
            }
            ViewData["status"] = new SelectList(this.GetStatus(), "status", "status");
            ViewData["driverid"] = new SelectList(await driverRepo.GetForDropDown(), "id", "name");
            return View(shipment);
        }

        // GET: Shipments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
           if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var shipment = await shipmentRepo.GetById(id);
            if (shipment == null)
            {
                return NotFound();
            }
            ViewData["driverid"] = new SelectList(await driverRepo.GetForDropDown(), "id", "name", shipment.driverid);
            return View(shipment.MapForUpdate());
        }

        // POST: Shipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UpdateShipmentRequest model)
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
                    var updatedShipment = await shipmentRepo.Update(model.Map());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await ShipmentExists(model.id)))
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
            ViewData["driverid"] = new SelectList(await driverRepo.GetForDropDown(), "id", "name", model.driverid);
            return View(model);
        }

        // GET: Shipments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            try 
            {
                if (string.IsNullOrEmpty(id))
                    return NotFound();

                var deletedShipment = await shipmentRepo.Remove(id);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        
        private async Task<bool> ShipmentExists(string id)
        {
          return ((await shipmentRepo.GetAllReadOnly()).Any(e => e.id == id));
        }

        private List<Status> GetStatus()
        {
            return new List<Status> { Status.init, Status.pickup, Status.delivered, Status.returned };
        }
    }
}
