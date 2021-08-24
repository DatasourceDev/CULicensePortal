using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CULCS.DAL;
using CULCS.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
//using CULCS.Identity;
using CULCS.DTO;
using CULCS.AD;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.TokenCacheProviders.InMemory;
using Microsoft.Identity.Web.UI;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Graph;
using System.Net;

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Polly;
using Polly.Extensions.Http;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace CULCS
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
           

            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CuContext>(options => options.UseSqlServer(connectionString));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<ILoginServices, LoginServices>();
            services.AddTransient<LoginServices>();

            services.AddTransient<IAuthenticationProvider, GraphAuthenticationProvider>();
            services.AddTransient<IGraphServiceClient, GraphServiceClient>();
            services.AddTransient<IGraphProvider, MicrosoftGraphProvider>();
            services.AddHostedService<GetGraphItemsWorker>();

            services.AddHttpClient("RestClient")
                    .AddPolicyHandler((service, request) => HttpPolicyExtensions.HandleTransientHttpError()
                            .WaitAndRetryAsync(new[]
                            {
                                TimeSpan.FromSeconds(1),
                                TimeSpan.FromSeconds(5),
                                TimeSpan.FromSeconds(10),
                                TimeSpan.FromSeconds(20),
                                TimeSpan.FromSeconds(30)
                            },
                            onRetry: (outcome, timespan, retryAttempt, context) =>
                            {
                                int LOG_THRESHOLD = 1000;
                                if (timespan.TotalMilliseconds > LOG_THRESHOLD)
                                {
                                    service.GetService<ILogger>()
                                        .LogWarning("Delaying for {delay}ms, then making retry {retry}.", timespan.TotalMilliseconds, retryAttempt);
                                }
                            }
                        ));

            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = 100000; // 10000 items max
                options.ValueLengthLimit = 1024 * 1024 * 500; // 100MB max len form data
            });

            services.Configure<CULCS.DTO.AzureAD>(Configuration.GetSection("AzureAd"));

            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/Auth/Login", "");
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
                options.LogoutPath = "/Auth/Logout";
            });


            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Auth/Login";
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

            //app.UseAdMiddleware();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CuContext>();
                context.Database.EnsureCreated();
                context.Database.Migrate();
                context.EnsureSeedData();
            }

            string enUSCulture = "en-US";
            var supportedCultures = new[]
            {
                new CultureInfo("th-TH"),
                new CultureInfo("th"),
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUSCulture),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Auth}/{action=Login}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
