using Microsoft.EntityFrameworkCore;
using SchoolProject.Data;

namespace SchoolProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSession(opt =>
            {

                opt.IdleTimeout = TimeSpan.FromMinutes(60); 
            });


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDBContext>(
               opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
               );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=LandingHome}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
