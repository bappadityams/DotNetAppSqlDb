 using Microsoft.Owin;
 using Microsoft.Owin.Extensions;
 using Microsoft.Owin.Security;
 using Microsoft.Owin.Security.Cookies;
 using Microsoft.Owin.Security.OpenIdConnect;
 using Microsoft.IdentityModel.Tokens;
 using Owin;
 using System.Configuration;

[assembly: OwinStartup(typeof(DotNetAppSqlDb.Startup))]

namespace DotNetAppSqlDb
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = ConfigurationManager.AppSettings["ClientId"],
                    Authority = ConfigurationManager.AppSettings["Authority"],
                    RedirectUri = ConfigurationManager.AppSettings["RedirectUri"],
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "roles"
                    }
                });

            // This makes any middleware defined above this line run before the Authorization rule is applied in web.config
            app.UseStageMarker(PipelineStage.Authenticate);
        }
    }
}
