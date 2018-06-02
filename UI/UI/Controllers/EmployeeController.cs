using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: UI
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:10031");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/employeedoc").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<employees>>().Result;
            }
            else
            {
                ViewBag.result = "Error";
            }
            return View();
        }
        [HttpGet]
        public ActionResult CreateNewInfor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateNewInfor(employees employee)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var result = client.PostAsJsonAsync("http://localhost:10031/api/EmployeeDoc/CreateNewInfor", employee).Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Result = "Successfully saved!";
                    ModelState.Clear();
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    ViewBag.Result = "ID is duplicated. Input ID again, please";
                }
            }
            return View(employee);
        }
    }
}