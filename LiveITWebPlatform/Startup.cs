using Application.Security;
using Infrastructure.Entities;
using LiveIT.AuthorizationRequirements;
using LiveIT.Data;
using LiveIT.Security;
//using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using System;
using System.Security.Claims;

namespace LiveIT
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("live-it");
            services.AddDbContext<liveitContext>(options => options.UseSqlServer(connection));

            services.AddDbContext<AppDbContext>(config => {
                config.UseInMemoryDatabase("Memory");
            });

            //AddIdentity registers the services
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "LIVEIT.Cookie";
                config.LoginPath = "/Auth/Login";
            });

            services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            services.AddAuthorization(config =>
            {
                config.AddPolicy("Claim.Role", policyBuilder =>
                {
                    policyBuilder.RequireCustomClaim(ClaimTypes.Role);
                });
            });

            services.AddScoped<IAuthorizationHandler, CustomRequireClaimHandler>();

            services.AddTransient<GoogleReCAPTCHAService>();

            services.AddMvc(options =>
              options.EnableEndpointRouting = false)
              .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSingleton<UniqueCode>();
            services.AddSingleton<CustomIDataProtection>();

            services.AddSession(options =>
            {
                // Set session timeout value
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });

            services.Configure<ReCAPTCHASettings>(Configuration.GetSection("GoogleReCAPTCHA"));

            services.AddMailKit(config => config.UseMailKit(Configuration.GetSection("Email").Get<MailKitOptions>()));

            services.AddControllersWithViews().AddRazorRuntimeCompilation();//ONLY FOR THE DEVELOPMENT (addrazorRuntimeCompilation)
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Auth/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMvcWithDefaultRoute();
            app.UseRouting();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Toolkit}/{action=Index}/{id?}");
            });
        }
    }
}
