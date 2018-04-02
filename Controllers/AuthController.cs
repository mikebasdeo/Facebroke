using System.Threading.Tasks;
using Facebroke.API.Data;
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
        public async Task<IActionResult> Register(string username, string password){
            //TODO:validate request

            //store all usernames as lowercase.
            username = username.ToLower();

            if(await _repo.UserExists(username))
            {
                return BadRequest("Username is already taken.");
            }

            //create a new user
            var userToCreate = new User{
                Username = username
            };

            var createdUser = await _repo.Register(userToCreate, password);

            //return successful creation.
            return StatusCode(201);

        }
        

        
    }
}