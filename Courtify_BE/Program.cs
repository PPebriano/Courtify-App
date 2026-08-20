
using CourtifyBE.Data;
using CourtifyBE.Repositories;
using CourtifyBE.Services;
using Microsoft.EntityFrameworkCore;

namespace CourtifyBE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<CourtifyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Courtify")));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddScoped<IVenueService, VenueService>();
            builder.Services.AddScoped<IBookingService, BookingService>();

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", policy =>
                {
                    policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                });
            }
    );

    //        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //.AddJwtBearer(options =>
    //{
    //    options.TokenValidationParameters = new TokenValidationParameters
    //    {
    //        ValidateIssuer = true,
    //        ValidateAudience = true,
    //        ValidateLifetime = true,
    //        ValidateIssuerSigningKey = true,
    //        ValidIssuer = builder.Configuration["Jwt:Issuer"],
    //        ValidAudience = builder.Configuration["Jwt:Audience"],
    //        IssuerSigningKey = new SymmetricSecurityKey(
    //            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    //    };
    //});

            var app = builder.Build();

            app.UseCors("AllowAngular");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
