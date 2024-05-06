
using DemoAPI.ActionFilters;
//using DemoAPI.Controllers.Action;
using DemoAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DemoAPI
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
            builder.Services.AddSwaggerGen(options => options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml")));

            // builder.Services.AddSqlServer<DemoContext>(builder.Configuration.GetConnectionString("DemoContext"));

            builder.Services.AddSqlServer<DemoAPIContext>(builder.Configuration.GetConnectionString("MyConn"));

            builder.Services.AddScoped(typeof(MyLogResultFilter));
          //builder.Services.AddScoped(typeof(Multipleof500Filter);


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Test", builder =>
                {
                  //  builder.AllowAnyMethod(); //All- Get, Post Put, Delete All Are Allowed.
                  builder.WithMethods("GET", "POST");
                    builder.AllowAnyOrigin(); //All origins are Allowed
                  //  builder.AllowAnyHeader();
                });

                options.AddPolicy("Sercure", builder =>
                {
                    builder.AllowAnyOrigin(); //All origins are Allowed
                    //  builder.WithOrigins("https://www.mywebsite.com", "https://someTestingserver.com")
                    // .WithHeaders("Content-Type", "authorization", "Accept")
                   //.WithMethods("GET", "POST");
                });
            });
            builder.Services.AddMemoryCache(); //enable in memory caching
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors("Test"); //loose security
            }
            else
            {
                app.UseCors("Secure");
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        
        }
    }
}
