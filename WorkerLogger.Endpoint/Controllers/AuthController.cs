using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkerLogger.Domain.Entities.ApplicationUsers;
using WorkerLogger.Domain.Entities.Authentication;

namespace WorkerLogger.Endpoint.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDataModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            //Regisztráció
            if (user == null)
            {
                var createUser = new ApplicationUser
                {
                    UserName = model.UserName.ToLower(),
                    Email = model.UserName.ToLower(),
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                //
                var result = await userManager.CreateAsync(createUser, model.Password);


                return Ok();
            }

            return BadRequest();
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserDataModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);

            

            //Bejelentkezás
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var claim = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                };
                foreach (var role in await userManager.GetRolesAsync(user))
                {
                    claim.Add(new Claim(ClaimTypes.Role, role));
                }
                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("biztonsagostitkoskod"));
                var token = new JwtSecurityToken(
                    issuer: "http://www.security.org", audience: "http://www.security.org",
                    claims: claim, expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    UserId = user.Id
                });
            }


            return Unauthorized();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserInfos()
        {
            var user = userManager.Users.FirstOrDefault(x => x.UserName == this.User.Identity.Name);
            if (user != null) {
                return Ok(new
                {
                    UserName = user.UserName

                });
            }

            return BadRequest();
        }
    }
}
