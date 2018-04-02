/**
-This is a fucking table!
-Need to give it context (Let app know this is going to a type of database, which is done in DataContext)
 */

namespace Facebroke.API.Models
{
    public class Value
    {

        //Primary key for the sqlite database. (*prop*)
        public int Id{get; set;}
        public string Name{get; set;}


    }
}