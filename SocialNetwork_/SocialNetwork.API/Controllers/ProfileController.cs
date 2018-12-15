using SocialNetwork.API.Models;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;
using System.Security.Claims;

namespace SocialNetwork.API.Controllers
{
    [Authorize]
    [RoutePrefix("API/Profile")]
    public class ProfileController : ApiController
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        
        [AllowAnonymous] //allows that anyone sees all profiles, including garden photos
        [Route("List")]
        [HttpGet]
        public IList<UProfile> List()
        => _dbContext.Profile.ToList();
        
    }
}
