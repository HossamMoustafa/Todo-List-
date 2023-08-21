
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Task.Models;
using Task.Repository;
using Task.Services;

namespace Task
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

            
            /// register service for database 
            builder.Services.AddDbContext<ApplicationDbContext>( options =>
                {
                    var key = builder.Configuration.GetConnectionString("cs"); 

                    options.UseSqlServer(key);
                }) ;

            // register
            builder.Services.AddScoped<ITodoRepository,TodoRepository>();

            // cors service /// Asdfghjk011211@

            builder.Services.AddCors(); 


            builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>() ;


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.SaveToken = true; 
                options.RequireHttpsMetadata= true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {

                    ValidateIssuer = false,
                    ValidIssuer = builder.Configuration["Jwt:ValidIssu"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:validAud"],

                    IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"])),

                    ValidateIssuerSigningKey = true  // to validate key 

                }; 
            }); 


           var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors( c=> c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}