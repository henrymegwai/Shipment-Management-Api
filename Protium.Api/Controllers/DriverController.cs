using Data.Entities;
using Data.Mapper;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Protium.Api.Helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Protium.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IRepository<Driver> driverRepo;
        public DriverController(IRepository<Driver> _driverRepo)
        {
            driverRepo = _driverRepo;
        }
        // GET: api/<DriverController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var drivers = await driverRepo.GetAllReadOnly();
                return Ok(new { Message = "driver(s) retrieved successfully", Data = drivers.Select(x => x.Map()) });
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
                    throw new Exception("driver id is required");
                var driver = await driverRepo.GetById(id);
                return Ok(new { Message = "driver retrieved successfully", Data = driver.Map() });
            }
            catch (Exception ex)
            {
                Response<string> response = new Response<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }

        }
        // POST api/<DriverController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDriverRequest model)
        {
            try
            {
                model.Validate();
                var driver = await driverRepo.Add(model.Map());
                return Ok(new { Message = "driver created successfully", Data = driver.Map() });
            }
            catch (Exception ex)
            {
                Response<string> response = new Response<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateDriverRequest model)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new Exception("driver id is required");
                model.Validate();
                var updatedDriver = await driverRepo.Update(model.Map());
                return Ok(new { Message = "driver updated successfully", Data = updatedDriver });
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
                    throw new Exception("driver id is required");
                var deletedDriver = await driverRepo.Remove(id);
                return Ok(new { Message = "driver deleted successfully" });
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
