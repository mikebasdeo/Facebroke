using System.Threading.Tasks;
using Facebroke.API.Data;
using Facebroke.API.Dtos;
using Facebroke.API.Models;
using Microsoft.AspNetCore.Mvc;

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
            //TODO:validate request

            //store all usernames as lowercase.
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if(await _repo.UserExists(userForRegisterDto.Username))
            {
                return BadRequest("Username is already taken.");
            }

            //create a new user
            var userToCreate = new User{
                Username = userForRegisterDto.Username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            //return successful creation.
            return StatusCode(201);

        }
        

        
    }
}