using HOSPITALMANAGEMENT.Data.Domain;
using HOSPITALMANAGEMENT.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HOSPITALMANAGEMENT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public CartController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetCart")]
        public async Task<IEnumerable<CartModel>> GetCart()
        {
            return await _dbContext.cartTable.ToListAsync();
        }

        [HttpPost("AddCart")]
        public async Task<IActionResult> AddCart([FromBody] CartModel product)
        {
            try
            {
                // Ensure ModelState is valid before proceeding
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Set CreatedAt and UpdatedAt properties
                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = null; // You might want to set this to some default value

                // Save the product to the database
                var addedProduct = _dbContext.cartTable.Add(product).Entity;
                await _dbContext.SaveChangesAsync();

                return Ok(addedProduct);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception details
                Console.WriteLine($"DbUpdateException: {ex.Message}");

                // Check if there are inner exceptions
                Exception innerException = ex;
                while (innerException != null)
                {
                    Console.WriteLine($"Inner Exception: {innerException.Message}");
                    innerException = innerException.InnerException;
                }

                return StatusCode(500, $"An error occurred while adding the product to the cart: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log other types of exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, $"An error occurred while adding the product to the cart: {ex.Message}");
            }
        }
        [HttpPut("UpdateCart")]
        public async Task<IActionResult> UpdateCart([FromBody] List<CartModel> cartItems)
        {
            try
            {
                // Ensure ModelState is valid before proceeding
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Iterate through the provided cart items
                foreach (var cartItem in cartItems)
                {
                    // Retrieve the existing cart item from the database based on its ID
                    var existingCartItem = await _dbContext.cartTable.FindAsync(cartItem.Id);

                    if (existingCartItem != null)
                    {
                        // Update the stock quantity or other properties as needed
                        existingCartItem.StockQuantity = cartItem.StockQuantity;
                        // You can update other properties similarly

                        // Set the UpdatedAt property
                        existingCartItem.UpdatedAt = DateTime.Now;

                        // Update the database
                        _dbContext.Entry(existingCartItem).State = EntityState.Modified;
                    }
                    else
                    {
                        // Handle the case where the cart item with the provided ID is not found
                        return NotFound($"Cart item with ID {cartItem.Id} not found.");
                    }
                }

                // Save changes to the database
                await _dbContext.SaveChangesAsync();

                return Ok(cartItems);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception details
                Console.WriteLine($"DbUpdateException: {ex.Message}");

                // Check if there are inner exceptions
                Exception innerException = ex;
                while (innerException != null)
                {
                    Console.WriteLine($"Inner Exception: {innerException.Message}");
                    innerException = innerException.InnerException;
                }

                return StatusCode(500, $"An error occurred while updating the cart: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log other types of exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, $"An error occurred while updating the cart: {ex.Message}");
            }
        }


        [HttpDelete("DeleteCart/{cartItemId}")]
        public async Task<IActionResult> DeleteCart(int cartItemId)
        {
            try
            {
                // Ensure ModelState is valid before proceeding
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Find the cart item by its ID
                var cartItemToDelete = await _dbContext.cartTable.FindAsync(cartItemId);

                if (cartItemToDelete == null)
                {
                    return NotFound(new { success = false, message = "No cart item found with the provided ID." });
                }

                // Remove the cart item from the database
                _dbContext.cartTable.Remove(cartItemToDelete);
                await _dbContext.SaveChangesAsync();

                return Ok(new { success = true, message = "Cart item deleted successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, new { success = false, message = $"An error occurred while deleting the cart item: {ex.Message}" });
            }
        }

        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            try
            {
                // Retrieve all cart items
                var allCartItems = await _dbContext.cartTable.ToListAsync();

                if (allCartItems == null || allCartItems.Count == 0)
                {
                    return NotFound(new { success = false, message = "No cart items found to delete." });
                }

                // Remove all cart items from the database
                _dbContext.cartTable.RemoveRange(allCartItems);
                await _dbContext.SaveChangesAsync();

                return Ok(new { success = true, message = "All cart items deleted successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, new { success = false, message = $"An error occurred while deleting all cart items: {ex.Message}" });
            }
        }


    }
}
