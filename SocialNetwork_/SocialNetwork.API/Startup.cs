using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

// This class Startup.cs is going to replace Global.asax and will start our ASP.NET Web API using OWIN
    [assembly: OwinStartup(typeof(SocialNetwork.API.Startup))]

namespace SocialNetwork.API    
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            ConfigureAuth(app);

            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}