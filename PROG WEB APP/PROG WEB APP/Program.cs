using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PROG_WEB_APP.DATA;
using PROG_WEB_APP.Models;
using PROG_WEB_APP.Services;


namespace PROG_WEB_APP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);




            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            //make sure to add other services if they are cretaed 
            builder.Services.AddScoped<ProductService>();


            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=WebAppDb.db"));

            builder.Services.AddIdentity<Employee, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
.AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();


           


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
