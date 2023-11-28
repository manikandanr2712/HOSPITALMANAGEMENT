using HOSPITALMANAGEMENT.Data.Domain;
using HOSPITALMANAGEMENT.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace HOSPITALMANAGEMENT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productsController : ControllerBase
    {

        private readonly dbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public productsController(dbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        //[HttpPost]
        //[Route("upload")]
        //public async Task<IActionResult> UploadImage(IFormFile file)
        //{
        //    try
        //    {
        //        if (file == null || file.Length == 0)
        //        {
        //            return BadRequest("No file uploaded.");
        //        }

        //        // Generate a unique file name
        //        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

        //        // Define the path where the image will be saved
        //        var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        //        // Save the image to the specified path
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }

        //        // Return the URL of the uploaded image
        //        var imageUrl = $"https://yourdomain.com/images/{fileName}";

        //        return Ok(imageUrl);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while uploading the image.");
        //    }
        //}

        [HttpPost("upload")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UploadImage([FromForm] ProductModel product)
        {
            try
            {
                if (product.ImageFile != null && product.ImageFile.Length > 0)
                {
                    // Generate a unique filename for the image
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.ImageFile.FileName);

                    // Set the image path in the model
                    product.ImagePath = Path.Combine("Assets", fileName);
                    //product.Id = 0;

                    // Save the image to the specified path
                    using (var stream = new FileStream(product.ImagePath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(stream);
                    }

                    // Save the product to the database
                    _context.ProductsTable.Add(product);
                    await _context.SaveChangesAsync();

                    return Ok("Image uploaded successfully.");
                }
                else
                {
                    return BadRequest("No image file provided.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while uploading the image: {ex.Message}");
            }
        }

        [HttpGet("get")]
 // Assuming user policy is for general users
        public async Task<IActionResult> GetProductById()
        {
            try
            {
                // Retrieve the product from the database by ID
                var product = await _context.ProductsTable.ToListAsync();

                if (product == null)
                {
                    return NotFound($"Product with ID not found.");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the product: {ex.Message}");
            }
        }

        [HttpGet("get/{id}")]
        [Authorize(Policy = "UserPolicy")] // Assuming user policy is for general users
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                // Retrieve the product from the database by ID
                var product = await _context.ProductsTable.FindAsync(id);

                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found.");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the product: {ex.Message}");
            }
        }

        [HttpPut("update/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductModel updatedProduct)
        {
            try
            {
                // Check if the provided ID matches an existing product
                var existingProduct = await _context.ProductsTable.FindAsync(id);
                if (existingProduct == null)
                {
                    return NotFound($"Product with ID {id} not found.");
                }

                // Update the existing product properties
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Description = updatedProduct.Description;
                existingProduct.Price = updatedProduct.Price;
                // Update other properties as needed

                // Save changes to the database
                await _context.SaveChangesAsync();

                return Ok("Product updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the product: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                // Check if the provided ID matches an existing product
                var existingProduct = await _context.ProductsTable.FindAsync(id);
                if (existingProduct == null)
                {
                    return NotFound($"Product with ID {id} not found.");
                }

                // Remove the product from the context
                _context.ProductsTable.Remove(existingProduct);

                // Save changes to the database
                await _context.SaveChangesAsync();

                return Ok("Product deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the product: {ex.Message}");
            }
        }

    }
}