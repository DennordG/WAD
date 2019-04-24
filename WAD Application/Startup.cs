using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WAD_Application.Data;
using WAD_Application.Services.Interfaces;
using WAD_Application.Services;
using WAD_Application.Repositories;
using WAD_Application.Models;
using WAD_Application.Repositories.Interfaces;

namespace WAD_Application
{
	public class Startup
	{
		private const string DefaultConnection = "DefaultConnection";

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString(DefaultConnection)));

			services.AddTransient<IService<UserConversation>, UserConversationService>();
			services.AddTransient<IService<User>, UserService>();
			services.AddTransient<IService<Conversation>, ConversationService>();
			services.AddTransient<IService<Message>, MessageService>();
			services.AddTransient<IService<Content>, ContentService>();

			services.AddTransient<IRepository<UserConversation>, Repository<UserConversation>>();
			services.AddTransient<IRepository<User>, Repository<User>>();
			services.AddTransient<IRepository<Conversation>, Repository<Conversation>>();
			services.AddTransient<IRepository<Message>, Repository<Message>>();
			services.AddTransient<IRepository<Content>, Repository<Content>>();

			services.AddTransient<IUnitOfWork, UnitOfWork>();

			services.AddDefaultIdentity<IdentityUser>()
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
