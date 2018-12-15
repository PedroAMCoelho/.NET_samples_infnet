using SocialNetwork.API.Models;
using SocialNetwork.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SocialNetwork.Web.Models.Profile;
using SocialNetwork.Web.Helpers;
using SocialNetwork.Web.Models.Account;
using SocialNetwork.Web.Attributes;
using SocialNetwork.Blobs;

namespace SocialNetwork.Web.Controllers
{
    public class HomeController : Controller
    {
         //GET: Home
       // public ActionResult Index()
        //{
         //   return View();
       // }

        private HttpClient _client;
        private TokenHelper AccessToken;

        public HomeController()
        {
            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            _client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:44319/")
            };
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(mediaType);

            AccessToken = new TokenHelper();
        }
        public async Task<ActionResult> Index()
        {
            try
            {
                //_client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", AccessToken));
                //var listResponse = await _client.GetAsync("/API/Profile/LoggedUser"); //isso aqui seria pra 1 user soh, mas vou usar outro approach
                var listResponse = await _client.GetAsync("/API/Profile/List");
                //PostAsync("API/Profile/List", null);

                var responseContent = await listResponse.Content.ReadAsAsync<IList<ProfileViewModel>>();
                //var responseContent = await listResponse.Content.ReadAsAsync<ProfileViewModel>(); //isso aqui seria pra 1 user soh

                return View(responseContent);
            }
            catch (Exception)
            {
                return View(new List<ProfileViewModel>());
            }

        }
        [Authentication]
        [HttpGet]
        public async Task<ActionResult> ShowLoggedUserInfo()
        {
            try
            {
                _client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", AccessToken.AccessToken));
                var response = await _client.GetAsync("/API/Account/LoggedUser"); //isso aqui seria pra 1 user soh, mas vou usar outro approach
                //var listResponse = await _client.GetAsync("/API/Profile/List");
                //PostAsync("API/Profile/List", null);

                //var responseContent = await listResponse.Content.ReadAsAsync<IList<ProfileViewModel>>();
                var responseContent = await response.Content.ReadAsAsync<LoggedUserViewModel>(); //isso aqui seria pra 1 user soh

                return View(responseContent);
            }
            catch (Exception)
            {
                return View(new LoggedUserViewModel());
            }

        }

        //this method gets the changes and send it to the API
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public async Task<ActionResult> Edit(LoggedUserViewModel model)
        { /*
            _client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", AccessToken.AccessToken));

            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("/API/Account/SaveDataChanges", model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            return View(model);
        } */

            _client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", AccessToken.AccessToken));
          //upload
            var blob = new AzureBlobs();
            var filePath = await blob.UploadFile(model.UploadPictureUrl);
            model.PictureUrl = filePath;

            var data = new Dictionary<string, string>()
                {
                    { "UserName", model.Email },                    
                    { "Email", model.Email },
                    { "GardenOwnerName", model.GardenOwnerName },
                    { "PictureUrl", model.PictureUrl },
                    { "GardenName", model.GardenName },
                    { "GardenDescription", model.GardenDescription },
                    { "MainGarden", model.MainGarden },
                    { "SubGarden", model.SubGarden },
                    { "GardenLocation", model.GardenLocation }
                };

            using (var requestContent = new FormUrlEncodedContent(data))
            {
                var response = await _client.PostAsync("/API/Account/SaveDataChanges", requestContent); // era model aqui e n requestContent

                if (response.IsSuccessStatusCode)
                {
                    //return View("Login");
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

    } 
}