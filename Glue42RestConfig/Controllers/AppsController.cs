using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApplication6.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AppsController : ApiController
    {
        private string AppsFolder = HttpRuntime.AppDomainAppPath + "config\\apps";

        [Route("apps/search")]
        public dynamic Get()
        {
            // not used in this example, but you can use this to return user-specific applications
            string currentUser = GetCurrentUser();

            List<dynamic> apps = new List<dynamic>();

            foreach (string file in Directory.EnumerateFiles(AppsFolder, "*.json"))
            {
                string contents = File.ReadAllText(file);
                try
                {
                    dynamic appsInFile = JsonConvert.DeserializeObject(contents);
                    if (appsInFile is JArray)
                    {
                        foreach (dynamic app in appsInFile)
                        {
                            dynamic fdc3 = WrapGlueAppConfigIntoFDC3(app);
                            apps.Add(fdc3);
                        }
                    }
                    else if (appsInFile is JObject)
                    {
                        dynamic fdc3 = WrapGlueAppConfigIntoFDC3(appsInFile);
                        apps.Add(fdc3);
                    }
                }
                catch
                {
                    // 
                }
            }
            return new { applications = apps.ToArray() };
        }

        private dynamic WrapGlueAppConfigIntoFDC3(dynamic config)
        {
            return new
            {
                name = config.name,
                version = "1",
                title = config.title,
                manifestType = "Glue42",
                manifest = JsonConvert.SerializeObject(config)
            };
        }

        private string GetCurrentUser()
        {
            if (Request.Headers.Contains("user"))
            {
                return Request.Headers.GetValues("user").FirstOrDefault();
            }
            return String.Empty;
        }
    }
}
