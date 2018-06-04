using System;
using System.Threading.Tasks;
using AspNetCore.Jwt.Sample.Logic;
using AspNetCore.Jwt.Sample.Models.Client;
using AspNetCore.Jwt.Sample.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Auth
{
    /// <summary>
    /// Users Controller
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/users")]
    public sealed class UserController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class
        /// </summary>
        /// <param name="userManager">User manager</param>
        public UserController(UserManager userManager)
        {
            Manager = userManager;
        }

        private UserManager Manager { get; set; }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="request">Registration details</param>
        /// <returns>
        /// HTTP 200 if successful
        /// HTTP 400 if the post body contains validation errors
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody]RegistrationRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var user = new User()
            {
                UserName = request.Email,
                Email = request.Email,
                GivenName = request.GivenName,
                Surname = request.Surname
            };

            var result = await Manager.Create(user, request.Password);

            if (!result.Succeeded)
            {
                AddIdentityErrorsToModelState(result);
                return BadRequest(ModelState);
            }

            return Ok();
        }

        private void AddIdentityErrorsToModelState(IdentityResult result)
        {
            if (result.Errors == null) return;

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
            }
        }
    }
}
