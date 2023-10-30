using HOSPITALMANAGEMENT.Data.Domain;
using HOSPITALMANAGEMENT.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HOSPITALMANAGEMENT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public DiseaseController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetDiseases")]
        public async Task<IEnumerable<Disease>> GetDisease()
        {
            return await _dbContext.Diseases.ToListAsync();
        }

        [HttpPost]
        [Route("AddDisease")]
        public async Task<ActionResult<Disease>> AddDisease(Disease disease)
        {
            try
            {
                _dbContext.Diseases.Add(disease);
                await _dbContext.SaveChangesAsync();
                return Ok(disease); // Return 200 OK on success
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error"); // Handle exceptions and return a 500 response if needed
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiseaseAsync(int id)
        {
            try
            {
                var disease = await _dbContext.Diseases.FindAsync(id);

                if (disease == null)
                {
                    return NotFound(); // Return a 404 response if the disease doesn't exist
                }

                _dbContext.Diseases.Remove(disease); // Use Remove to delete
                await _dbContext.SaveChangesAsync(); // Save changes to the database asynchronously

                return NoContent(); // Return a 204 response on successful deletion
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error"); // Handle exceptions and return a 500 response if needed
            }
        }
    }
}
