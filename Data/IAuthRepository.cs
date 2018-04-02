/*Interface for all the tasks the user can perform with the database. Uses the Repository design pattern. */

using System.Threading.Tasks;
using Facebroke.API.Models;

namespace Facebroke.API.Data
{
    public interface IAuthRepository
    {

        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);

        Task<bool> UserExists(string username);
         
    }
}