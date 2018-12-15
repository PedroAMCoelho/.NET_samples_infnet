using Newtonsoft.Json;
using SocialNetwork.Web.Attributes;
using SocialNetwork.Web.Helpers;
using SocialNetwork.Web.Models.Authorr;
using SocialNetwork.Web.Models.Book;
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
    public class BookController : Controller
    {

        private HttpClient _client;
        private TokenHelper tokenHelper;

        public BookController()
        {
            _client = new HttpClient();

            _client.BaseAddress = new Uri("https://localhost:44319");
            _client.DefaultRequestHeaders.Clear();

            tokenHelper = new TokenHelper();
            _client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", tokenHelper.AccessToken));

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(mediaType);                       

        }

        // GET: Book
        public ActionResult Index()
        {
            return View(); //deletar isso
        }

        // GET: Book
        public ActionResult AddBook()
        {
            return View();
        }

        ///POST: /Book/AddBook
        [HttpPost]
        [AuthenticationAttribute]
        public async Task<ActionResult> AddBook(AddBookViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return View();
            }

            HttpResponseMessage response = await _client.PostAsJsonAsync("/API/Book/AddBook", model);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Home");
            }
            return View(model);
        }

        //DELETE: Book/Delete
        [AuthenticationAttribute]        
        public async Task<ActionResult> Delete(int BookId)
        {
            HttpResponseMessage response = await _client.DeleteAsync("/API/Book/Delete?BookId=" + BookId);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Error");
            }
        }

        [AuthenticationAttribute]
        public async Task<ActionResult> Edit(int BookId)
        {
            //HttpResponseMessage response = await _client.GetAsync("/API/Book/Update?BookId=" + BookId);
            await ShowBookDetails(BookId);

             return View();
        }

        //this method gets the changes and send it to the API
        //UPDATE: Book/Update
        [AuthenticationAttribute]
        [HttpPost]
        public async Task<ActionResult> Edit(AddBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            HttpResponseMessage response = await _client.PutAsJsonAsync("/API/Book/Update", model);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        //GET: Book/ListBooks
        public async Task<ActionResult> ListBooks()
        {
            try
            {
                //_client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", AccessToken));
                //var listResponse = await _client.GetAsync("/API/Profile/LoggedUser"); //isso aqui seria pra 1 user soh, mas vou usar outro approach
                var listResponse = await _client.GetAsync("/API/Book/ListBooks");
                //PostAsync("API/Profile/List", null);

                var responseContent = await listResponse.Content.ReadAsAsync<IList<AddBookViewModel>>();
                //var responseContent = await listResponse.Content.ReadAsAsync<ProfileViewModel>(); //isso aqui seria pra 1 user soh

                return View(responseContent);
            }
            catch (Exception)
            {
                return View(new List<AddBookViewModel>());
            }

        }

        //GET: Book/Details
        [AuthenticationAttribute]
        [HttpGet]
        public async Task<ActionResult> ShowBookDetails(int BookId)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("/API/Book/Details?BookId=" + BookId);
                                    
                var responseContent = await response.Content.ReadAsAsync<AddBookViewModel>(); //isso aqui seria pra 1 user soh
                            
                return View(responseContent);
            }
            catch (Exception)
            {
                return View(new AddBookViewModel());
            }

        }             

    }
}