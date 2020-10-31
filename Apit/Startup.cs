using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Apit.Service;
using DatabaseLayer;
using DatabaseLayer.Entities;
using BusinessLayer;
using BusinessLayer.Interfaces;
using BusinessLayer.Repositories;

namespace Apit
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime.
        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Set configuration singleton from appsettings.json file
            var projConfig = new ProjectConfig();
            Configuration.Bind("Project", projConfig);
            services.AddSingleton(projConfig);

            var SECURITY = new SecurityConfig();
            Configuration.Bind("Security", SECURITY);

            services.AddTransient<MailService>();

            // Join interfaces and implementations
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<ITopicsRepository, TopicsRepository>();
            services.AddTransient<IArticlesRepository, ArticlesRepository>();
            services.AddTransient<IConferencesRepository, ConferenceRepository>();
            services.AddScoped<DataManager>();

            // Connect to our database (edit connection string from "appsettings.json" file)
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("DatabaseLayer")));

            services.AddDefaultIdentity<User>(options =>
                {
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(SECURITY.Lockout.LockoutTimeSpan);
                    options.Lockout.MaxFailedAccessAttempts = SECURITY.Lockout.MaxFailedAccessAttempts;
                    options.Lockout.AllowedForNewUsers = SECURITY.Lockout.AllowedForNewUsers;

                    options.Password.RequiredLength = SECURITY.Password.RequiredLength;
                    options.Password.RequiredUniqueChars = SECURITY.Password.RequiredUniqueChars;
                    options.Password.RequireNonAlphanumeric = SECURITY.Password.RequireNonAlphanumeric;
                    options.Password.RequireLowercase = SECURITY.Password.RequireLowercase;
                    options.Password.RequireUppercase = SECURITY.Password.RequireUppercase;
                    options.Password.RequireDigit = SECURITY.Password.RequireDigit;

                    options.User.RequireUniqueEmail = SECURITY.User.RequireUniqueEmail;

                    options.SignIn.RequireConfirmedPhoneNumber = SECURITY.SignIn.RequireConfirmedPhoneNumber;
                    options.SignIn.RequireConfirmedAccount = SECURITY.SignIn.RequireConfirmedAccount;
                }).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/account/access-denied";
                options.Cookie.Name = SECURITY.User.CookieName;
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(SECURITY.User.ExpireTimeSpanMinutes);
                options.LoginPath = "/account/login";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = SECURITY.User.SlidingExpiration;
            });

            bool res = Enum.TryParse<PasswordHasherCompatibilityMode>
                (SECURITY.Password.HasherCompatibilityMode, out var hasherMode);
            if (!res) throw new ArgumentException(nameof(SECURITY.Password.HasherCompatibilityMode));
            services.Configure<PasswordHasherOptions>(options =>
            {
                options.CompatibilityMode = hasherMode;
                options.IterationCount = SECURITY.Password.HasherIterationCount;
            });


            // Configure auth policy for Admin Area
            services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminArea",
                    policy => policy.RequireRole("superman"));
            });

            // To use MVC
            services.AddControllersWithViews(options =>
            {
                options.Conventions.Add(
                    new AdminAuthorization("Admin", "AdminArea"));
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();

            // To use .cshtml easily 
            services.AddRazorPages();
        }


        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/home/error");
                // The default HSTS value is 30 days.
                // You may want to change this for production scenarios,
                // see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // To use local (static) files from wwwroot folder  (.css, .png, etc)
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Admin",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}