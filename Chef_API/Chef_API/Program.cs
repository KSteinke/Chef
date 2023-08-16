using Chef_API.Data;
using Chef_API.Repositories;
using Chef_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Security.Claims;
using Chef_API.Services.TokenAuthentication.Interfaces;
using Chef_API.Services.TokenAuthentication;

namespace Chef_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            
           

            builder.Services.AddTransient<ITokenManager, TokenManager>();

            //builder.Services.AddAuthentication(o =>
            //{
            //    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddCookie(x =>
            //{
            //    x.Cookie.Name = "Token";
            //}).AddJwtBearer(o =>
            //{
            //    o.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Config.JwtTokenKey),
            //        ValidateLifetime = true,
            //        ValidateAudience = false,
            //        ValidateIssuer = false,
            //        ClockSkew = TimeSpan.Zero

            //    };
            //    o.Events = new JwtBearerEvents
            //    {
            //        OnMessageReceived = context =>
            //        {
            //            context.Token = context.Request.Cookies["Token"];
            //            return Task.CompletedTask;
            //        }
            //    };
            //});

            builder.Services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Config.JwtTokenKey),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero

                };
            });


            //Test policy for Token wrapped in cookie
            builder.Services.AddAuthorization(o =>
            {
                o.AddPolicy("TestPolicy", policy => policy.RequireClaim(ClaimTypes.Name, "Gordon"));
                
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContextPool<ChefDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ChefConnection"))
            ); //Database connection, DbContext initial config
            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>(); //Dependency Injection configuration for RecipeRepositories
            builder.Services.AddScoped<IChefRepository, ChefRepository>();
            builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(policy => policy.WithOrigins("http://localhost:7093", "https://localhost:7093").AllowAnyMethod().WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization));
            
            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}