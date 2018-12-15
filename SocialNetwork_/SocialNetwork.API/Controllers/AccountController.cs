using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using SocialNetwork.API.Models;
using SocialNetwork.API.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using SocialNetwork.API.Models.Account;
using Microsoft.Owin.Security.Cookies;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace SocialNetwork.API.Controllers
{
    [Authorize]
    [RoutePrefix("API/Account")]
    public class AccountController : ApiController
    {
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //POST API/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                GardenOwnerName = model.GardenOwnerName,
                Profile = new UProfile
                {
                    PictureUrl = model.PictureUrl,
                    GardenName = model.GardenName,
                    GardenDescription = model.GardenDescription,
                    MainGarden = model.MainGarden,
                    SubGarden = model.SubGarden,
                    GardenLocation = model.GardenLocation
                }
            };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }

        //POST API/Account/Logout
        [HttpPost]
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        //método auxiliar para a verificação de erros
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return BadRequest(ModelState);
            }
            return null;
        }

        //GET API/Account/LoggedUser
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("LoggedUser")]
        [HttpGet]
        public LoggedUserBindingModel GetLoggedUserInfo()
        {

            ApplicationUser user = UserManager.FindByEmail(User.Identity.GetUserName());

            if(user == null)
            {
                return null;
            }
            
            return new LoggedUserBindingModel
            {
                    Email = user.Email,
                    GardenOwnerName = user.GardenOwnerName,
                    PictureUrl = user.Profile.PictureUrl,
                    GardenName = user.Profile.GardenName,
                    GardenDescription = user.Profile.GardenDescription,
                    MainGarden = user.Profile.MainGarden,
                    SubGarden = user.Profile.SubGarden,
                    GardenLocation = user.Profile.GardenLocation                
            };
        }

        [Route("SaveDataChanges")]
        [HttpPost]
        //[AllowAnonymous]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public async Task<IHttpActionResult> SaveProfile(LoggedUserBindingModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApplicationUser user = UserManager.FindByEmail(User.Identity.GetUserName());
            var loggedUserProfile = _dbContext.Profile.FirstOrDefault(c => c.ProfileId == user.Profile.ProfileId);
            var loggedUser = _dbContext.Users.Find(user.Id);
        
            loggedUser.Email = model.Email;
            loggedUser.GardenOwnerName = model.GardenOwnerName;
            loggedUserProfile.PictureUrl = model.PictureUrl;
            loggedUserProfile.GardenName = model.GardenName;
            loggedUserProfile.GardenDescription = model.GardenDescription;
            loggedUserProfile.MainGarden = model.MainGarden;
            loggedUserProfile.SubGarden = model.SubGarden;
            loggedUserProfile.GardenLocation = model.GardenLocation;
            //user.Profile.GardenLocation = model.GardenLocation; estava usando assim antes

            await _dbContext.SaveChangesAsync();

            return Ok();
        }



        protected override void Dispose(bool disposing)
        {
            if(disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }
            base.Dispose(disposing);
        }
    }
        
    
}
