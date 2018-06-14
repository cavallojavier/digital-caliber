using System.Threading.Tasks;
using AutoMapper;
using digital.caliber.model.Data;
using digital.caliber.model.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace digital.caliber
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
            services.AddAutoMapper();
            services.AddMvc();

            services.AddDbContext<CaliberDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

            services.AddOptions();

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim("rol", "api_access"));
                options.AddPolicy("AdminUser", policy => policy.RequireClaim("su", "admin_access"));
            });

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<CaliberDbContext>()
                .AddDefaultTokenProviders();

            //var tokenValidationParameters = new TokenValidationParameters
            //{
            //    ValidateIssuer = true,
            //    ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],
            //    ValidateAudience = true,
            //    ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],
            //    ValidateIssuerSigningKey = true,
            //    IssuerSigningKey = _signingKey,
            //    RequireExpirationTime = false,
            //    ValidateLifetime = false,
            //    ClockSkew = TimeSpan.Zero
            //};

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddCookie("hdc_login", options =>
            {
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/login");
                options.LogoutPath = new Microsoft.AspNetCore.Http.PathString("/logout");
            });

            services.ConfigureApplicationCookie(o =>
            {
                o.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login/");
                o.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Forbidden");
                o.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = (ctx) =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }

                        return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = (ctx) =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 403;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                //Apis route
                routes.MapRoute(
                name: "apis",
                template: "api/{controller}/{action}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
