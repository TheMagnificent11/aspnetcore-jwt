using System;
using System.Threading.Tasks;
using AspNetCore.Jwt.Sample.Logic;
using AspNetCore.Jwt.Sample.Models.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Auth
{
    /// <summary>
    /// Token Controller
    /// </summary>
    [ApiController]
    [Route("api/token")]
    public sealed class TokenController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenController"/> class
        /// </summary>
        /// <param name="manager">User manager</param>
        public TokenController(UserManager manager)
        {
            Manager = manager;
        }

        private UserManager Manager { get; set; }

        /// <summary>
        /// Returns an access token if valid authentication details are provided
        /// </summary>
        /// <param name="request">Login request</param>
        /// <returns>
        /// HTTP 200 containing access token if valid authentication details are provided
        /// HTTP 400 if invalid authentication details are provided
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<AuthToken>> Post([FromBody]LoginRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var token = await Manager
                .SignIn(request.Email, request.Password)
                .ConfigureAwait(false);
            if (token == null) return BadRequest();

            return Ok(token);
        }
    }
}
