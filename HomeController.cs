using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using MyStmt.ViewModels;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Web.Security;
using MyStmt.Models;


namespace MyStmt.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.ResetPassword = "";
            return View();
        }
        [HttpPost]
        public ActionResult Index(string CompanyID, string AgentID, string Password, int PageSize)
        {
            try
            {
                ViewBag.ResetPassword="";
                ViewBag.CompanyID = CompanyID;
                ViewBag.AgentID = AgentID;

                string url;
                HttpClient myHttpClient = new HttpClient();
                HttpResponseMessage response;

                //
                //Step1: Call API
                //
                url = "https://apiserver.com/";
                myHttpClient.BaseAddress = new Uri(url);
                response = myHttpClient.GetAsync("PROFILE?CID=" + CompanyID + "&AID=" + AgentID + "&PSWD=" + Uri.EscapeDataString(Password)).Result;

                if (response.IsSuccessStatusCode)
                {
                    string profile = response.Content.ReadAsStringAsync().Result;
                    if (profile.IndexOf("ExceptionMsg") >= 0)
                    {
                        ModelState.AddModelError("Error", "Invalid Login Information");
                    }
                    else
                    {
                        ProfileViewModel pf = JsonConvert.DeserializeObject<ProfileViewModel> (profile);
                        System.Web.HttpContext.Current.Session["Profile"] = pf;
                        if (pf == null)
                        {
                            ModelState.AddModelError("Error", "Invalid Login Information");
                        }                       
                        else
                        {
                            System.Web.HttpContext.Current.Session["AgentID"] = AgentID;
                            System.Web.HttpContext.Current.Session["CompanyId"] =CompanyID;
                            System.Web.HttpContext.Current.Session["AgentName"] = pf.fullName;
                            System.Web.HttpContext.Current.Session["ClientID"] = pf.ClientID;
                            System.Web.HttpContext.Current.Session["PWD"] = Password;
                            if (pf.ResetPSWD == "Y")
                            {
                                ViewBag.ResetPassword = pf.ResetPSWD;
                                return View();
                            }

                            //
                            // Call API
                            //
                            myHttpClient = new HttpClient();
                            url = "https://apiserver.com/";
                            myHttpClient.BaseAddress = new Uri(url);
                            response = myHttpClient.GetAsync("LEDGER?CID=" + CompanyID + "&AID=" + Uri.EscapeDataString(AgentID) + "&PSWD=" + Uri.EscapeDataString(Password)).Result;

                            if (response.IsSuccessStatusCode)
                            {
                                string LedgerList = response.Content.ReadAsStringAsync().Result;
                                if (LedgerList.IndexOf("eAC Based") > 0)
                                {
                                    return Redirect("https://???");
                                }
                                else if (LedgerList.IndexOf("ExceptionMsg") >= 0)
                                {
                                    ModelState.AddModelError("Error", "Invalid Login Information");
                                }
                                else
                                {
                                    List<LedgerViewModel> Ledgers = JsonConvert.DeserializeObject<List<LedgerViewModel>>(LedgerList);
                                    TempData["LedgerViewModel"] = Ledgers;                                   
                                    FormsAuthentication.SetAuthCookie(AgentID, false);
                                    return RedirectToAction("List", "Ledgers", new { PageSize = PageSize });
                                }


                            }
                            else
                            {
                                ModelState.AddModelError("Error", "Invalid Login Information");
                            }
                        }
                    }


                }
                else
                {
                    ModelState.AddModelError("Error", "Invalid Login Information");
                }

                
        }
             catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Invalid Login Information");
            }
                       
            return View();
        }


        public ActionResult Signout()
        {
            this.Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

       
    }
}