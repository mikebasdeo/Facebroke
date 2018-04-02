using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Facebroke.API.Data;
using Facebroke.API.Dtos;
using Facebroke.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Facebroke.API.Controllers
{

    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        //attributes
        private readonly IAuthRepository _repo;


        //constructors
        public AuthController(IAuthRepository repo){
            _repo = repo;
        }


        //methods
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto){


            //store all usernames as lowercase.
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();



            if(await _repo.UserExists(userForRegisterDto.Username))
            {
                ModelState.AddModelError("Username", "Username already exists.");
            }

           
            //validate request using DTO validators
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }


            //create a new user
            var userToCreate = new User{
                Username = userForRegisterDto.Username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            //return successful creation.
            return StatusCode(201);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginDto userForLoginDto)
        {

            //get user from repo and try to login.
            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);


            //if not successful,
            if(userFromRepo == null)
            {
                return Unauthorized();
            }


            //If successful, create a JSON Web Token for speedy future authentication
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("super secret key");


            //what do we want inside our token?
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepo.Username)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)

            };

            //Now create the token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok( new {tokenString});

        }
        

        
    }
}