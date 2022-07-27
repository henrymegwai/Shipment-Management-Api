using Data.Entities;
using Data.Models;
using Data.Repositories;
using Data.Mapper;
using Microsoft.AspNetCore.Mvc;
using Protium.Api.Helper;
using Data.Helper;

namespace Protium.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IRepository<Shipment> shipmentRepo;
        public ShipmentController(IRepository<Shipment> _shipmentRepo)
        {
            shipmentRepo = _shipmentRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var shipments = await shipmentRepo.GetAllReadOnly();
                return Ok(new { Message = "shipment(s) retrieved successfully", Data = shipments.Select(x=>x.Map())});
            }
            catch (Exception ex)
            {
                Response<string> response = new Response<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            } 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new Exception("shipment id is required");
                var shipment = await shipmentRepo.GetById(id);
                return Ok(new { Message = "shipment retrieved successfully", Data = shipment.Map() });
            }
            catch (Exception ex)
            {
                Response<string> response = new Response<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateShipmentRequest model)
        {
            try
            {
                model.Validate(); 
                var shipment = await shipmentRepo.Add(model.Map());
                return Ok(new { Message = "shipment created successfully", Data = shipment.Map() });
            }
            catch (Exception ex)
            {
                Response<string> response = new Response<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateShipmentRequest model)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new Exception("shipment id is required");
                model.Validate();
                var updatedshipment = await shipmentRepo.Update(model.Map());
                return Ok(new { Message = "shipment updated successfully", Data = updatedshipment });
            }
            catch (Exception ex)
            {
                Response<string> response = new Response<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            } 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new Exception("shipment id is required");
                var deletedShipment = await shipmentRepo.Remove(id);
                return Ok(new { Message = "shipment deleted successfully" });
            }
            catch (Exception ex)
            {
                Response<string> response = new Response<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
            
        }

        [HttpPut("{id}/Pickup")]
        public async Task<IActionResult> Pickup(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new Exception("shipment id is required");
                var updatedshipment = await shipmentRepo.UpdateStatus(id,Status.pickup);
                return Ok(new { Message = "shipment status updated to pickup successfully", Data = updatedshipment });
            }
            catch (Exception ex)
            {
                Response<string> response = new Response<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPut("{id}/Delivered")]
        public async Task<IActionResult> Delivered(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new Exception("shipment id is required");
                var updatedshipment = await shipmentRepo.UpdateStatus(id, Status.delivered);
                return Ok(new { Message = "shipment status updated to delivered successfully", Data = updatedshipment });
            }
            catch (Exception ex)
            {
                Response<string> response = new Response<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPut("{id}/Returned")]
        public async Task<IActionResult> Returned(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new Exception("shipment id is required");
                var updatedshipment = await shipmentRepo.UpdateStatus(id, Status.returned);
                return Ok(new { Message = "shipment status updated to returned successfully", Data = updatedshipment });
            }
            catch (Exception ex)
            {
                Response<string> response = new Response<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
    }
}
