using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Net.Http;
using MyStmt.ViewModels;
using MyStmt.Models;

namespace MyStmt.Controllers
{
    public class reqhandlerController : Controller
    {
        // GET: reqhandler
        [HttpPost]
        public string Index(string CurPSWD, string newPSWD)
        {
            //
            // Step1: compare the new and old password
            //
            string oldPWD = System.Web.HttpContext.Current.Session["PWD"].ToString();
            if (oldPWD != CurPSWD)
            {
                return "Current password incorrect";
            }


            //
            //Step22: Call API to change PSWD
            //
            string sResult = "Fail";
            HttpClient myHttpClient = new HttpClient();
            string url = "https://apiserver.com/";
            myHttpClient.BaseAddress = new Uri(url);

            ProfileViewModel pf = (ProfileViewModel) System.Web.HttpContext.Current.Session["Profile"];
            var content = new { APP = "MyStmt", SQL = Uri.EscapeDataString(pf.SqlServerIp), DB = Uri.EscapeDataString(pf.DBName),
                                AID = pf.AgentRID, UID = "", PSWD = Uri.EscapeDataString(newPSWD),
                                Phone = pf.CoPhone, Email = pf.CoEmail}; 
            
            HttpResponseMessage response = myHttpClient.PutAsJsonAsync("api/APL_NEW_PSWD", content).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                if (result.ToUpper().IndexOf("OK") >= 0)
                {
                    sResult = "OK";
                }
            }

            return sResult;
        }
    }
}