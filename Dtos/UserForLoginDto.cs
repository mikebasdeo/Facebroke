using System.ComponentModel.DataAnnotations;

namespace Facebroke.API.Dtos
{
    public class UserForLoginDto
    {
        //attributes
        public string Username { get; set; }
        public string Password { get; set; }


    }
}