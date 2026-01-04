using Microsoft.EntityFrameworkCore;
using PersonPicker.Data;

namespace PersonPicker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var corsPolicyName = "AllowFrontend";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(corsPolicyName, policy =>
                {
                    policy
                        .WithOrigins(builder.Configuration["settings:frontend"] ?? "http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            if (builder.Environment.IsProduction())
            {
                builder.WebHost.ConfigureKestrel(options =>
                {
                    options.ListenAnyIP(
                        int.Parse(builder.Configuration["settings:port"] ?? "6500")
                    );
                });
            }
            var app = builder.Build();

            app.UseCors(corsPolicyName);

            app.MapControllers();

            app.Run();
        }
    }
}