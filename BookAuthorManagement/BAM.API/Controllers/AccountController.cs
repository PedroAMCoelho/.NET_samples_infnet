using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using SocialNetwork.Core.Models;
using SocialNetwork.Core.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using SocialNetwork.Core.Models.Account;
using Microsoft.Owin.Security.Cookies;
using System.Web;

namespace SocialNetwork.Core.Controllers
{
    [Authorize]
    [RoutePrefix("API/Account")]
    public class AccountController : ApiController
    {
        private ApplicationUserManager _userManager;

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
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if(!result.Succeeded)
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
