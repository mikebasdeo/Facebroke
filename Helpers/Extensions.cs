using Microsoft.AspNetCore.Http;

namespace Facebroke.API.Helpers
{
    public static class Extensions
    {
        //this will add global errors to the header of the http response.
        public static void AddApplicationError(this HttpResponse response, string message){
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}