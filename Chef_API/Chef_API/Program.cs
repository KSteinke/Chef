using Chef_API.Data;
using Chef_API.Repositories;
using Chef_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace Chef_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContextPool<ChefDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ChefConnection"))
            ); //Database connection, DbContext initial config
            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>(); //Dependency Injection configuration for RecipeRepositories
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(policy => policy.WithOrigins("http://localhost:7093", "https://localhost:7093").AllowAnyMethod().WithHeaders(HeaderNames.ContentType));
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}