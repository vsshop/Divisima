using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebApplication48.Services;

namespace WebApplication48
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://*:80");
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


            // Add services to the container.
            builder.Services.AddControllersWithViews().AddViewLocalization();
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/";
                    options.AccessDeniedPath= "/";
                });

            builder.Services.AddDbContext<EntityDatabase>();

            builder.Services.AddScoped<ProductServices>();
			builder.Services.AddScoped<UserService>();
			builder.Services.AddScoped<AuthorizationService>();
			builder.Services.AddScoped<PaymentService>();
			builder.Services.AddScoped<GoogleService>();

            var app = builder.Build();

            CultureInfo[] allCultures = new CultureInfo[]
            {
                new CultureInfo("en"),
                new CultureInfo("uk"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions()
            {
                SupportedCultures = allCultures,
                SupportedUICultures = allCultures,
                DefaultRequestCulture = new RequestCulture("uk"),
            });



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "product",
                pattern: "{product?}/{controller=Home}/{action=Product}");

            app.Run();
        }
    }
}