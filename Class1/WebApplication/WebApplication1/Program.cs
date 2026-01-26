
using Microsoft.EntityFrameworkCore;
using WebApplication1.Persistence;

namespace WebApplication1
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

            builder.Services.AddDbContext<StudentsContext>(options =>
            options.UseNpgsql("User ID =postgres;Password=qwerty;Server=localhost;Port=5432;Database=StudentsDB;Include Error Detail=true;"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetService<StudentsContext>();
            context!.Database.EnsureCreated();

            app.Run();
        }
    }
}
