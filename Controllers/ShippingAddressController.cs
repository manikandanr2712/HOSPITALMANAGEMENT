using HOSPITALMANAGEMENT.Data.Domain;
using HOSPITALMANAGEMENT.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HOSPITALMANAGEMENT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingAddressController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public ShippingAddressController(dbContext context)
        {
            _dbContext = context;
        }

        [HttpPost]
        [Route("PostShippingAddress")]
        public IActionResult PostShippingAddress([FromBody] shippingAddress shippingAddress)
        {
            try
            {
                _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[InsertShippingAddress] @Name, @Mobile, @Email, @Country, @State, @City, @Street, @ZipCode, @UserId",
                    new SqlParameter("@Name", shippingAddress.Name),
                    new SqlParameter("@Mobile", shippingAddress.Mobile),
                    new SqlParameter("@Email", shippingAddress.Email),
                    new SqlParameter("@Country", shippingAddress.Country),
                    new SqlParameter("@State", shippingAddress.State),
                    new SqlParameter("@City", shippingAddress.City),
                    new SqlParameter("@Street", shippingAddress.Street),
                    new SqlParameter("@ZipCode", shippingAddress.ZipCode),
                    new SqlParameter("@UserId", shippingAddress.UserId)
                // ... other parameters
                );

                return Ok(shippingAddress);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetShippingAddressByUserId/{userId}")]
        public IActionResult GetShippingAddressByUserId(string userId)
        {
            try
            {
                var userIdParameter = new SqlParameter("@UserId", userId);

                // Calling the stored procedure using FromSqlRaw
                var result = _dbContext.shippingAddresses.FromSqlRaw("EXEC [dbo].[GetShippingAddressByUserId] @UserId", userIdParameter)
                                                         .ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
