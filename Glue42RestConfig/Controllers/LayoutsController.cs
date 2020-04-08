using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApplication6.Controllers
{
    /// <summary>
    /// This controller implements:
    /// GET layouts -> returns all layouts. In a real implementation this should consider the user that is passed in the request headers
    /// POST layouts -> creates/updates a layout
    /// DELETE layouts -> removes a layout (does nothing)
    /// </summary>
    public class LayoutsController : ApiController
    {
        private string LayoutsFolder = HttpRuntime.AppDomainAppPath + "config\\layouts";

        // GET api/values
        [Route("layouts")]
        [HttpGet]
        public dynamic Get()
        {
            // not used in this example, but you can use this to return user-specific layouts
            string currentUser = GetCurrentUser();

            List<dynamic> layouts = new List<dynamic>();
            var files = Directory.EnumerateFiles(LayoutsFolder, "*.json");

            foreach (string file in files)
            {
                string contents = File.ReadAllText(file);
                try
                {
                    dynamic layout = JsonConvert.DeserializeObject(contents);
                    layouts.Add(layout);
                }
                catch
                {
                    // TODO handle error
                }
            }
            return new { layouts = layouts.ToArray() };
        }

        [Route("layouts")]
        [HttpPost]
        public async void Post()
        {
            string currentUser = GetCurrentUser();

            string bodyStr = await Request.Content.ReadAsStringAsync();
            dynamic body = JObject.Parse(bodyStr);
            if (body.layout == null)
            {
                throw new ArgumentException("layouts missing in body");
            }
            dynamic layout = body.layout;
            if (layout == null)
            {
                throw new ArgumentException("layouts missing in body");
            }
            string fileName = layout.name + ".json";
            string filePath = Path.Combine(LayoutsFolder, fileName);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(layout));
        }


        [Route("layouts")]
        [HttpDelete]
        public void Delete(int id)
        {
            string currentUser = GetCurrentUser();
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
