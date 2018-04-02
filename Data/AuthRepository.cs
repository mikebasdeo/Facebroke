/*Concrete implementation of the database tasks.
 */

using System;
using System.Threading.Tasks;
using Facebroke.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Facebroke.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        //attributes
        private readonly DataContext _context;

        //constructors
        public AuthRepository(DataContext context){
            _context = context;
        }


        //methods
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            //handle no username found
            if(user == null){
                return null;
            }
          
            //handle incorrect password
            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)){
                return null;
            }

            //handle correct login
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            //check password here
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < computedHash.Length; i++){

                    if(computedHash[i] != passwordHash[i]){
                        return false;
                    }
                }
                return true;
            }

        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            createPasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

           await _context.Users.AddAsync(user);
           await _context.SaveChangesAsync();

           return user;

        }

        private void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = hmac.Key;
                passwordSalt = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }


        }

        public async Task<bool> UserExists(string username)
        {
            //check if user exists.
            if(await _context.Users.AnyAsync(x => x.Username == username)){
                return false;
            }
            return true;
        }
    }
}