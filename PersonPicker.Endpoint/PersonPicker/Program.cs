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
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseCors(corsPolicyName);
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
