using System;
using System.Linq;
using System.Web.Http;

namespace Glue42RestConfig.Controllers
{
    public static class Extensions
    {
        public static string GetCurrentUser(this ApiController apiController)
        {
            // If running with windows auth
            if (apiController.User.Identity.Name != null)
            {
                return apiController.User.Identity.Name;
            }

            // Glue42 will send the user name of the call as part of the request headers
            if (apiController.Request.Headers.Contains("user"))
            {
                return apiController.Request.Headers.GetValues("user").FirstOrDefault();
            }
            // legacy support
            if (apiController.Request.Headers.Contains("impersonated_user"))
            {
                return apiController.Request.Headers.GetValues("impersonated_user").FirstOrDefault();
            }
            return String.Empty;
        }
    }
}