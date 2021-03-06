using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HomeworkAPI.Controllers
{
    public class APIController : Controller
    {
        // GET: API
        public ActionResult Index(string number)
        {

            var url = "https://imdb8.p.rapidapi.com/auto-complete";

            var client = new WebClient();

            var body = "";
            if (number != null && number != "")
            {
                body = client.DownloadString(url);

                JObject data = JObject.Parse(body);

                if ((bool)data["ok"] == true)
                {
                    ViewData["result"] = "Намерена е film за номер " + number;

                    ViewData["number"] = data["movie"]["movieNumber"];
                    ViewData["active"] = data["movie"]["validityDateToFormated"];
                    ViewData["class"] = data["movie"]["movieClass"];
                }
                else
                {
                    ViewData["result"] = "Не е намерена е film за показване " + number;
                }

                ViewData["body"] = body;

                return View();
            }

            public ActionResult Nasa()
            {
                var apiKey = "DEMO_KEY";

                var url = "https://api.nasa.gov/planetary/apod?api_key=" + apiKey;
                // https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);

                    var response = client.GetAsync("").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var body = response.Content.ReadAsStringAsync().Result;
                        JObject data = JObject.Parse(body);

                        ViewData["title"] = data["title"];
                        ViewData["description"] = data["explanation"];
                        ViewData["image"] = data["hdurl"];
                    }
                    else
                    {
                        ViewData["title"] = "There was an error.";
                    }
                }

                return View();
            }






        }
    }
}