using Newtonsoft.Json;
using SocialNetwork.Web.Attributes;
using SocialNetwork.Web.Helpers;
using SocialNetwork.Web.Models.Authorr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Web.Controllers
{
    [AuthenticationAttribute]
    public class AuthorController : Controller
    {

        private HttpClient _client;
        private TokenHelper tokenHelper;

        public AuthorController()
        {
            _client = new HttpClient();

            _client.BaseAddress = new Uri("https://localhost:44319");
            _client.DefaultRequestHeaders.Clear();

            tokenHelper = new TokenHelper();
            _client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", tokenHelper.AccessToken));

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(mediaType);

        }

        // GET: Author
        [AuthenticationAttribute]
        [HttpGet]
        public async Task<ActionResult> AuthorsList()
        {
            return View(await ListAuthors());
        }

        //GET: Author/ListAuthors
        [AuthenticationAttribute]        
        public async Task<IList<AuthorViewModel>> ListAuthors()
        {
            

            HttpResponseMessage response = await _client.GetAsync("/API/Author/ListAuthors");

            if (response.IsSuccessStatusCode)
            {
                var responseCont = await response.Content.ReadAsStringAsync();

                using (WebClient httpClient = new WebClient())
                {
                    return JsonConvert.DeserializeObject<IList<AuthorViewModel>>(responseCont);                    
                }
                
            }
            else
            {
                return null;
            }
        }

        //GET: Author/Details
        [AuthenticationAttribute]
        [HttpGet]
        public async Task<ActionResult> ShowAuthorDetails(int AuthorId)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("/API/Author/Details?AuthorId=" + AuthorId);

                var responseContent = await response.Content.ReadAsAsync<AuthorViewModel>();

                return View(responseContent);
            }
            catch (Exception)
            {
                return View(new AuthorViewModel());
            }

        }

        [AuthenticationAttribute]
        public async Task<ActionResult> Edit(int AuthorId)
        {            
            await ShowAuthorDetails(AuthorId);
            return View();
        }

        //this method gets the changes and send it to the API
        //UPDATE: Author/Update
        [AuthenticationAttribute]
        [HttpPost]
        public async Task<ActionResult> Edit(AuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            HttpResponseMessage response = await _client.PutAsJsonAsync("/API/Author/Update", model);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        //DELETE: Author/Delete
        [AuthenticationAttribute]
        public async Task<ActionResult> Delete(int AuthorId)
        {
            HttpResponseMessage response = await _client.DeleteAsync("/API/Author/Delete?AuthorId=" + AuthorId);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

    }
}