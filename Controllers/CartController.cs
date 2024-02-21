using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HOSPITALMANAGEMENT.Data.Domain;
using HOSPITALMANAGEMENT.Model;
using System.Linq;

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

        [HttpGet("GetCart/{userId}")]
        public async Task<IEnumerable<CartModel>> GetCart(string userId)
        {
            return await _dbContext.cartTable
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        [HttpPost("AddCart")]
        public async Task<IActionResult> AddCart([FromBody] CartModel cartItem)
        {
            try
            {
                // Ensure ModelState is valid before proceeding
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Set CreatedAt and UpdatedAt properties
                cartItem.CreatedAt = DateTime.Now;
                cartItem.UpdatedAt = null; // You might want to set this to some default value

                // Save the product to the database
                _dbContext.cartTable.Add(cartItem);
                await _dbContext.SaveChangesAsync();

                return Ok(cartItem);
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

                foreach (var updateData in cartItems)
                {
                    // Retrieve the existing cart item from the database based on its ID
                    var existingCartItem = await _dbContext.cartTable
                        .Include(c => c.User)
                        .FirstOrDefaultAsync(c => c.Id == updateData.Id);

                    if (existingCartItem != null)
                    {
                        // Update other fields as needed
                        existingCartItem.Name = updateData.Name;
                        existingCartItem.Description = updateData.Description;

                        // Update StockQuantity if it's present in updateData
                        if (updateData.StockQuantity.HasValue)
                        {
                            existingCartItem.StockQuantity = updateData.StockQuantity.Value;
                        }

                        // Set the UpdatedAt property
                        existingCartItem.UpdatedAt = DateTime.Now;

                        // Update the database
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        // Handle the case where the cart item with the provided ID is not found
                        return NotFound($"Cart item with ID {updateData.Id} not found.");
                    }
                }
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                // Log other types of exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, $"An error occurred while updating the cart items: {ex.Message}");
            }
        }

        [HttpDelete("DeleteCart/{userId}/{cartItemId}")]
        public async Task<IActionResult> DeleteCart(string userId, int cartItemId)
        {
            try
            {
                // Ensure ModelState is valid before proceeding
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Find the cart item by its ID and UserId
                var cartItemToDelete = await _dbContext.cartTable
                    .Where(c => c.Id == cartItemId && c.UserId == userId)
                    .FirstOrDefaultAsync();

                if (cartItemToDelete == null)
                {
                    return NotFound(new { success = false, message = "No cart item found with the provided ID and UserId." });
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

        [HttpDelete("DeleteAll/{userId}")]
        public async Task<IActionResult> DeleteAll(string userId)
        {
            try
            {
                // Retrieve all cart items for the specified user
                var userCartItems = await _dbContext.cartTable
                    .Where(c => c.UserId == userId)
                    .ToListAsync();

                if (userCartItems == null || userCartItems.Count == 0)
                {
                    return NotFound(new { success = false, message = "No cart items found to delete." });
                }

                // Remove all cart items for the specified user from the database
                _dbContext.cartTable.RemoveRange(userCartItems);
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
