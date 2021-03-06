﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Odbc;
using System.Net;
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
            OdbcConnection hiveConnection = new OdbcConnection("DSN=Hadoop Server;UID=hadoop;PWD=hadoop");
            hiveConnection.Open();

            Stream req = Request.InputStream;
            req.Seek(0, SeekOrigin.Begin);
            string request = new StreamReader(req).ReadToEnd();
            ContentResult response;
            string query;

            try
            {
                query = "INSERT INTO TABLE error_log (json_error_log) VALUES('" + request + "')";
                OdbcCommand command = new OdbcCommand(query, hiveConnection);
                command.ExecuteNonQuery();
                command.CommandText = query;
                response = new ContentResult { Content = "{status: 1}", ContentType = "application/json" };
                hiveConnection.Close();
                return response;
            }
            catch(WebException error)
            {
                response = new ContentResult { Content = "{status: 0, message:" + error.Message.ToString()+ "}" };
                System.Diagnostics.Debug.WriteLine(error.ToString());
                hiveConnection.Close();
                return response;
            }
        }
    }
}