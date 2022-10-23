using GearShiftAPI.Data;
using GearShiftAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GearShiftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly AppDBContext _context;

        public RentalController(AppDBContext appDBcontext)
        {
            _context = appDBcontext;
        }

        [HttpGet("allrentals")]
        public IActionResult GetAllRentals()
        {
            var rentals = _context.rentalModel.AsQueryable();
            return Ok(rentals);
        }

        [HttpGet("rentalbyid/{id}")]
        public IActionResult GetRentalById(int id)
        {
            var rental = _context.rentalModel.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return Ok(rental);
        }

        [HttpPost("createrental")]
        public IActionResult CreateRental([FromBody] RentalModel rentalObj)
        {
            rentalObj.RentalCost = Convert.ToDecimal(rentalObj.RentalCost);

            if(rentalObj == null)
            {
                return BadRequest();
            }
            else
            {
                _context.rentalModel.Add(rentalObj);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Rental added successfully"
                });
            }
        }
    }
}
