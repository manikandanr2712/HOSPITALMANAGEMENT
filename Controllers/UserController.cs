using HOSPITALMANAGEMENT.Model;
using HOSPITALMANAGEMENT.Model.DbModels;
using HOSPITALMANAGEMENT.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HOSPITALMANAGEMENT.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger logger;
        private readonly AuthService authService;

        public UserController(ILogger logger, AuthService authService)
        {
            this.authService = authService;
            this.logger = logger;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public ActionResult<User[]> GetAll()
        {
            try
            {
                var users = this.authService.GetAll();
                return Ok(users);
            }
            catch (Exception error)
            {
                logger.LogError(error.Message);
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPost("Role")]
        public ActionResult<User> ChangeRole(UserChangeRoleInputModel model)
        {
            try
            {
                var user = this.authService.ChangeRole(model.Email, model.Role);
                return Ok(user);
            }
            catch (Exception error)
            {
                logger.LogError(error.Message);
                return StatusCode(500);
            }
        }
    }
}