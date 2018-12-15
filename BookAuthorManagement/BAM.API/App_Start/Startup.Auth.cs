using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SocialNetwork.Core.App_Start;
using SocialNetwork.Core.Models;
using SocialNetwork.Core.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Core
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            //Configure the db context and user manager to use a single isntance per request][
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            //Configure the application for OAuth based flow
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                Provider = new ApplicationOAuthProvider(),
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1)
            };

            //Enable the application to use bearer tokens to authenticate users]
            app.UseOAuthBearerTokens(OAuthOptions);
        }

    }
}