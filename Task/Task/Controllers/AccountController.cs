using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using Task.DTOS;
using Task.Models;
using static System.Net.WebRequestMethods;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;
        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration config )
        {
             this.userManager = userManager;
            this.config = config;


        }

        [HttpPost("register")]
        public async Task<IActionResult?> Registeration(RegisterationUserDTo NewuserDto)
        {

            if(ModelState.IsValid)
            {
                // firstly map from NewuserDto to  applicationuser 

                ApplicationUser userModel = new ApplicationUser();
                userModel.Email = NewuserDto.Email;
                userModel.PasswordHash = NewuserDto.Password;
                userModel.Address = NewuserDto.Address;
                userModel.UserName= NewuserDto.UserName;    

                IdentityResult result = await userManager.CreateAsync(userModel, NewuserDto.Password); 
                

                if(result.Succeeded) {

                    return Ok("created success "); 
                }
                else
                {
                    return BadRequest(result.Errors.First());

                }

               
            }

            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult?> Login(LoginUserDto UserDto)
        {
            if (ModelState.IsValid)
            {
                /// check 

                ApplicationUser userModel = await userManager.FindByEmailAsync(UserDto.Email);

                var pass = await userManager.CheckPasswordAsync(userModel, UserDto.Password);

                if (userModel != null && pass )
                {
                    //add  claims  in token 

                    List<Claim> myclaims  = new List<Claim>();

                    myclaims.Add(new Claim (ClaimTypes.NameIdentifier, userModel.Id));
                    myclaims.Add(new Claim(ClaimTypes.Name, userModel.UserName));
                    myclaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    var authsecurityKey = 
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecurityKey"])); 

                    SigningCredentials credentials = 
                        new SigningCredentials(authsecurityKey, SecurityAlgorithms.HmacSha256); 

                    // create token
                    JwtSecurityToken mytoken = new JwtSecurityToken(

                        issuer: config["Jwt:ValidIssu"],
                        audience: config["Jwt:validAud"],
                        expires:DateTime.Now.AddHours(2), 
                       claims: myclaims,
                       signingCredentials: credentials 
                        );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                        expiration = mytoken.ValidTo
                    }); 
                     
                }
                return BadRequest("InValid Login Account");

            }
            else
            {

            }
            return BadRequest(ModelState); 

        }

    }
}
