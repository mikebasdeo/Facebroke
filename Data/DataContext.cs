
/**
- This will connect our models to a database. Super cool!
- We will have to add this as a service in Startup.cs
- We also have to add the database name to appsettings.json.
 */




using Facebroke.API.Models;
using Microsoft.EntityFrameworkCore;


namespace Facebroke.API.Data
{
    public class DataContext : DbContext
    {
        
        //attributes



        //constructors
        public DataContext(DbContextOptions<DataContext> options) : base(options){}




        //methods

        //this connects directly to our models, and sets the table name.
        public DbSet<Value> Values {get; set;}


    }
}