using AutoMapper;
using HOSPITALMANAGEMENT.Model.DbModels;
using HOSPITALMANAGEMENT.Model;
using HOSPITALMANAGEMENT.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HOSPITALMANAGEMENT.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private readonly AuthService authService;

        public AuthController(ILogger logger, AuthService authService, IMapper mapper)
        {
            this.authService = authService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
public ActionResult<object> Login(LoginInputModel userModel)
{
    try
    {
        if (ModelState.IsValid)
        {
            if (this.authService.IsAuthenticated(userModel.Email, userModel.Password))
            {
                var user = this.authService.GetByEmail(userModel.Email);
                var token = this.authService.GenerateJwtToken(userModel.Email, user.Role);

                        // Create an anonymous object to return both the token and the user's role.
                        var response = new
                        {
                            Token = token,
                            UserId = user.UserId,
                            Role = user.Role
                };

                return Ok(response);
            }
            return BadRequest("Email or password are not correct!");
        }

        return BadRequest(ModelState);
    }
    catch (Exception error)
    {
        logger.LogError(error.Message);
        return StatusCode(500);
    }
}

        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult<string> Register(RegisterInputModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (userModel.Password != userModel.ConfirmedPassword)
                    {
                        return BadRequest("Passwords does not match!");
                    }

                    if (this.authService.DoesUserExists(userModel.Email))
                    {
                        return BadRequest("User already exists!");
                    }

                    var mappedModel = this.mapper.Map<RegisterInputModel, User>(userModel);
                    mappedModel.Role = "User";
                    var user = this.authService.RegisterUser(mappedModel);

                    if (user != null)
                    {
                        var token = this.authService.GenerateJwtToken(user.Email, mappedModel.Role);
                        return Ok(user);

                    }

                    return BadRequest("Email or password are not correct!");
                }

                return BadRequest(ModelState);
            }
            catch (Exception error)
            {
                logger.LogError(error.Message);
                return StatusCode(500);
            }
        }
    }
}