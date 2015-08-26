using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Configuration;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.Framework.Runtime;
using Microsoft.Framework.OptionsModel;

namespace WebAPIApplication
{
	public interface IFoo
	{
		void DoSomething();
	}
	
	public class Foo : IFoo
	{
		public void DoSomething()
		{
			
		}
	}
	
    public partial class Startup
    {
		public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
			var configurationBuilder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
				.AddJsonFile("config.json")
				.AddEnvironmentVariables();
			Configuration = configurationBuilder.Build();
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
			
			services.Configure<Settings>(Configuration);
			services.AddSingleton<ISpeakerRepository, SpeakerRepository>();

            // Uncomment the following line to add Web API services which makes it easier to port Web API 2 controllers.
            // You will also need to add the Microsoft.AspNet.Mvc.WebApiCompatShim package to the 'dependencies' section of project.json.
            // services.AddWebApiConventions();
            //services.AddDataProtection();
            //  services.Configure<ExternalAuthenticationOptions>(options =>
            //  {
            //      options.SignInAsAuthenticationType = CookieAuthenticationDefaults.AuthenticationType;
            //  });
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use(async (httpcontext, next) =>  
            {
                // 1. Before the response
                Console.WriteLine("Before Response");
                
                // 2. Process the request and wait for completion
                await next.Invoke();
                
                // 3. After the response
                Console.WriteLine("After Response");
            });

			app.UseCookieAuthentication(null, IdentityOptions.ExternalCookieAuthenticationScheme); app.UseCookieAuthentication(null, IdentityOptions.TwoFactorRememberMeCookieAuthenticationScheme); app.UseCookieAuthentication(null, IdentityOptions.TwoFactorUserIdCookieAuthenticationScheme); 
            //  app.UseCookieAuthentication(options =>
			//  {
			//  	options.LoginPath = new PathString("/login");
			//  });
			
			//  app.Map("/login", socialApp =>
			//  {
			//  	socialApp.Run(async context =>
			//  	{
			//  		string authType = context.Request.Query["authtype"];
			//  		if (!string.IsNullOrEmpty(authType))
			//  		{
			//  			context.Response.Challenge(new AuthenticationProperties() { RedirectUri = "/" }, authType);
			//  			return;
			//  		}
	
			//  		context.Response.ContentType = "text/html";
			//  		await context.Response.WriteAsync("<html><body>");
			//  		await context.Response.WriteAsync("Choose an authentication type: <br>");
			//  		foreach (var type in context.GetAuthenticationTypes())
			//  		{
			//  			await context.Response.WriteAsync("<a href=\"?authtype=" + type.AuthenticationType + "\">" + (type.Caption ?? "(suppressed)") + "</a><br>");
			//  		}
			//  		await context.Response.WriteAsync("</body></html>");
			//  	});
			//  });
				
				
			//  app.Map("/logout", socialApp =>
			//  {
			//  	socialApp.Run(async context =>
			//  	{
			//  		context.Response.SignOut(CookieAuthenticationDefaults.AuthenticationType);
			//  		context.Response.ContentType = "text/html";
			//  		await context.Response.WriteAsync("<html><body>");
			//  		await context.Response.WriteAsync("You have been logged out. Goodbye " + context.User.Identity.Name + "<br>");
			//  		await context.Response.WriteAsync("<a href=\"/\">Home</a>");
			//  		await context.Response.WriteAsync("</body></html>");
			//  	});
			//  });
	
			//  // Deny anonymous request beyond this point.
			//  app.Use(async (context, next) =>
			//  {
			//  	if (!context.User.Identity.IsAuthenticated)
			//  	{
			//  		// The cookie middleware will intercept this 401 and redirect to /login
			//  		context.Response.Challenge();
			//  		return;
			//  	}
			//  	await next();
			//  });
            
            //  ConfigureAuth(app);

            // Configure the HTTP request pipeline.
            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc();
            // Add the following route for porting Web API 2 controllers.
            // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            
        }
    }
}
