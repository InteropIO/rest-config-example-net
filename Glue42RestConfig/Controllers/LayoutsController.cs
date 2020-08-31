using Glue42RestConfig.Controllers;
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
    [Authorize]
    public class LayoutsController : ApiController
    {
        private static string LayoutsFolder = HttpRuntime.AppDomainAppPath + "config\\layouts";
        private static string DefaultLayoutFile = LayoutsFolder + "\\default.layout";
        private readonly object defaultLayoutMutex = new object();

        // GET api/values
        [Route("layouts")]
        [HttpGet]
        public dynamic Get()
        {
            // not used in this example, but you can use this to return user-specific layouts
            string currentUser = this.GetCurrentUser();

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
            string currentUser = this.GetCurrentUser();

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
            string currentUser = this.GetCurrentUser();
            // Do nothing in this example
        }

        
        // GET api/values
        [Route("layouts/default")]
        [HttpGet]
        public dynamic GetDefaultLayout()
        {
            // not used in this example, but you can use this to return user-specific layouts
            string currentUser = this.GetCurrentUser();

            // read __default.layout file and return the string in it
            lock (defaultLayoutMutex)
            {
                try
                {
                    string defaultLayoutName = File.ReadAllText(DefaultLayoutFile);
                    return new { name = defaultLayoutName };
                }
                catch
                {                    
                }
                return null;
            }
        }

        [Route("layouts/default")]
        [HttpPost]
        public async void SaveDefaultLayout()
        {
            // save the name of the  in __default.layout file
            string bodyStr = await Request.Content.ReadAsStringAsync();
            dynamic body = JObject.Parse(bodyStr);
            string layoutName = body.name;
            lock (defaultLayoutMutex)
            {
                File.WriteAllText(DefaultLayoutFile, layoutName);
            }
        }
    }
}
