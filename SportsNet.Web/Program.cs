namespace SportsNet.Web
{
	using System.Reflection;

	using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using SportsNet.Data;
    using SportsNet.Data.Models;
    using SportsNet.Data.Repositories.Interfaces;
    using SportsNet.Data.Repositories;
    using SportsNet.Services.Data.Interfaces;
    using SportsNet.Web.Infrastructure.Extensions;
	using SportsNet.Web.ViewModels;
	using SportsNet.Services.Mapping;
    using Microsoft.AspNetCore.Mvc;

    using static Common.GeneralApplicationConstants;

    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<SportsNetDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount =
                       builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
                options.Password.RequireLowercase =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
                options.Password.RequireUppercase =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
                options.Password.RequireNonAlphanumeric =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                options.Password.RequiredLength =
                    builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<SportsNetDbContext>();

            builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            builder.Services.AddApplicationServices(typeof(IPostService));

            builder.Services.ConfigureApplicationCookie(cfg =>
            {
                cfg.AccessDeniedPath = "/Home/Error/401";
            });

            builder.Services
                .AddControllersWithViews()
                .AddMvcOptions(options =>
                {
                    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                });

            WebApplication app = builder.Build();

			AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

			if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error/500");
                app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.SeedAdministrator(AdminEmail);

            app.UseEndpoints(config =>
            {
				config.MapControllerRoute(
			       name: "areas",
			       pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}");

				config.MapControllerRoute(
                    name: "RouteTemplate",
                    pattern: "/{controller}/{action}/{id}/{information}",
                    defaults: new { Controller = "Category", Action = "Details" });
                config.MapDefaultControllerRoute();

                config.MapRazorPages();
            });
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}