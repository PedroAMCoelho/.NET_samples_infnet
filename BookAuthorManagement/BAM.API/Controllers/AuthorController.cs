using SocialNetwork.Core.Models;
using SocialNetwork.Core.Repository;
using SocialNetwork.Web.Models.Authorr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SocialNetwork.API.Controllers
{
    [Authorize]
    [RoutePrefix("API/Author")]
    public class AuthorController : ApiController
    {
        //GET: API/Author/ListAuthors
        [Route("ListAuthors")]
        [HttpGet]
        public IQueryable<Author> ListAuthors()
        {
           return new AuthorRepo().IQueryList();
        }

        //GET API/Details        
        [Route("Details")]
        [HttpGet]
        public async Task<AuthorViewModel> Details(int AuthorId)
        {
            Author author = new Author();
            author.AuthorId = AuthorId;

            return new AuthorRepo().Details(author);

        }

        //PUT API/Details
        [Route("Update")]
        [HttpPut]
        public IHttpActionResult Update(AuthorViewModel author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            new AuthorRepo().Update(author);

            return Ok();
        }

        //DELETE: API/Author/Delete?AuthorId={{value}}
        //DELETE: API/Author/Delete/{{value}}
        [Route("Delete")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int AuthorId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Author author = new Author();
            author.AuthorId = AuthorId;

            await new AuthorRepo().Delete(author);

            return Ok();
        }
    }
}
