using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin.Security.OAuth;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using SocialNetwork.API.App_Start;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace SocialNetwork.API.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        //chamado toda vez que precisamos verificar se o usuário é um usuário válido, geralmente quando ele realiza seu login
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            var user = await userManager.FindAsync(context.UserName, context.Password);

            if(user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);
            AuthenticationProperties properties = CreateProperties(user.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);

            context.Validated(ticket);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                {"userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}