using Facebroke.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Facebroke.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}


        //table names set here. After adding a new table, you must scaffold/migrate the new table. (dotnet ef migrations add *AddedUserModel*)
        //followed by (dotnet ef database update)
        public DbSet<Value> Values { get; set; }

        public DbSet<User> Users { get; set; }
    }
}