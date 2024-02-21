using HOSPITALMANAGEMENT.Data.Domain;
using HOSPITALMANAGEMENT.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HOSPITALMANAGEMENT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public DoctorController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetDoctors")]
        public async Task<IEnumerable<Doctor>> GetDoctor()
        {
            return await _dbContext.Doctor.ToListAsync();
        }

        [HttpPost]
        [Route("AddDoctor")]
        public async Task<ActionResult<Doctor>> AddDoctor(Doctor Doctor)
        {
            try
            {
                _dbContext.Doctor.Add(Doctor);
                await _dbContext.SaveChangesAsync();
                return Ok(Doctor); // Return 200 OK on success
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error"); // Handle exceptions and return a 500 response if needed
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctorAsync(int id)
        {
            try
            {
                var Doctor = await _dbContext.Doctor.FindAsync(id);

                if (Doctor == null)
                {
                    return NotFound(); // Return a 404 response if the Doctor doesn't exist
                }

                _dbContext.Doctor.Remove(Doctor); // Use Remove to delete
                await _dbContext.SaveChangesAsync(); // Save changes to the database asynchronously

                //return NoContent(); // Return a 204 response on successful deletion
                return Ok(id + " is deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error"); // Handle exceptions and return a 500 response if needed
            }
        }
    }
}
