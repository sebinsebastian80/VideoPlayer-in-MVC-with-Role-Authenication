using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using SampleProject.Models;

namespace SampleProject
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user    
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider    
            // Configure the sign in cookie    
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                LogoutPath = new PathString("/Account/LogOff"),
                ExpireTimeSpan = TimeSpan.FromMinutes(5.0),
                ReturnUrlParameter = "/Home/Index"
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            // Uncomment the following lines to enable logging in with third party login providers    
            //app.UseMicrosoftAccountAuthentication(    
            //  clientId: "",    
            //  clientSecret: "");    
            //app.UseTwitterAuthentication(    
            //  consumerKey: "",    
            //  consumerSecret: "");    
            //app.UseFacebookAuthentication(    
            //  appId: "",    
            //  appSecret: "");    
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "529463520135-u7da15aludvbp70rk1r53k11mlplbtlk.apps.googleusercontent.com",
                ClientSecret = "xevBk_O6k5V-62j0ve6eHoec"
            });
        }
    }
}