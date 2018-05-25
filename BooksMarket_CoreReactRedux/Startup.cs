using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksMarket_CoreReactRedux.DbRepositories;
using BooksMarket_CoreReactRedux.EF;
using BooksMarket_CoreReactRedux.Models;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BooksMarket_CoreReactRedux
{
    public static class AuthOptions
    {
        public static readonly string ISSUER = "MyAuthServer"; 
        public static readonly string AUDIENCE = "http://localhost:53050/"; 
        const string KEY = "mysupersecret_secretkey!123"; 
        public static readonly int LIFETIME = 1; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string defaultConnection = Configuration.GetConnectionString("DefaultConnection");

            services.AddLogging();
            services.AddDbContext<BooksContext>(options => options.UseSqlServer(defaultConnection));
            services.AddIdentity<User, UserRole>(opts =>
            {
                opts.Password.RequiredLength = 5;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
                opts.Lockout.MaxFailedAccessAttempts = 5;
                opts.Lockout.AllowedForNewUsers = true;
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

            })
            .AddEntityFrameworkStores<BooksContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });

            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
            services.ConfigureApplicationCookie(opts =>
            {
                opts.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            });

            //репозитории для работы с БД
            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IGenresRepository, GenresRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IBookOrdersRepository, BookOrdersRepository>();
            services.AddScoped<IMenuElementsRepository, MenuElementsRepository>();
            services.AddScoped<IRouteElementsRepository, RouteElementsRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();

            //т.к. сессия фактически хранится в кеше сервера, нужно подключить зависимости кеша
            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapSpaFallbackRoute(
                    name: "spa-fallback", 
                    defaults: new { controller = "Home", action = "Index" }); 
            });
            app.Use(next => context =>
            {
                var tokens = antiforgery.GetAndStoreTokens(context);
                context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
                    new CookieOptions()
                    {
                        HttpOnly = false
                    });
                return next(context);
            });
        }
    }
}
