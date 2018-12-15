using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using SocialNetwork.Core.Models;
using SocialNetwork.Core.Repository;
using SocialNetwork.Web.Models.Authorr;
using SocialNetwork.Web.Models.Book;

namespace SocialNetwork.API.Controllers
{
    [Authorize]
    [RoutePrefix("API/Book")]
    public class BookController : ApiController
    {              

        //POST API/Book/AddBook
        [Route("AddBook")]
        [HttpPost]
        public IHttpActionResult AddBook (AddBookViewModel book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            new BookRepo().Add(book);

            return Ok();
        }
        
        //GET: API/Book/ListBooks
        [Route("ListBooks")]
        [HttpGet]
        public IList<Book> ListBooks()
       => new BookRepo().List();


        //DELETE: API/Book/Delete?BookId={{value}}
        //DELETE: API/Book/Delete/{{value}}
        [Route("Delete")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int BookId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Book book = new Book();
            book.BookId = BookId;

           await new BookRepo().Delete(book);

            return Ok();
        }

        //PUT API/Update
        [Route("Update")]
        [HttpPut]
        public IHttpActionResult Update(AddBookViewModel book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            

            new BookRepo().Update(book);

            return Ok();
        }

        //GET API/Details        
        [Route("Details")]   
        [HttpGet]
        public async Task<AddBookViewModel> Details(int BookId)
        {              
            Book book = new Book();
            book.BookId = BookId;

            return new BookRepo().Details(book);         

        }        
    }
}