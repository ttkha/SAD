using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecruitmentUI.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RecruitmentUI.Controllers
{
    public class RecruitmentUIController : Controller
    {
        // GET: RecruitmentUI
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:4994");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/recruitment").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<recruitment>>().Result;
            }
            else
            {
                ViewBag.result = "Error";
            }
            return View();
        }
    }
}