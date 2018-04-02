/*
Data transfer objects are used to pass user objects to AuthController.cs
*/ 


using System.ComponentModel.DataAnnotations;

namespace Facebroke.API.Dtos
{
    public class UserForRegisterDto
    {
        //attributes
        [Required]
        public string Username { get; set; }


        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage="Password must be between 4 and 8 characters")]
        public string Password { get; set; }




        //constructors
        public UserForRegisterDto(string username, string password) 
        {
            this.Username = username;
            this.Password = password;
               
        }


        //methods

    }
}
