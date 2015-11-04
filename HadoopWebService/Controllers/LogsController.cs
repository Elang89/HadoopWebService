using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HadoopWebService.Controllers
{
    public class LogsController : Controller
    {
        // POST: HadoopRequest
        [HttpPost]
        public ContentResult Create(string json)
        {
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string request = new StreamReader(req).ReadToEnd();


            System.Diagnostics.Debug.WriteLine(request);
            ContentResult response = new ContentResult { Content = json, ContentType = "application/json" };
            return response;
        }
    }
}