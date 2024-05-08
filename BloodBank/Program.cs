using BloodBank.Mapping;
using BloodBank.Models;
using BloodBank.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace BloodBank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var ConnectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<Data>(
                     w => w.UseSqlServer(ConnectionStrings));
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAutoMapper(typeof(Auttomaper));
            builder.Services.AddTransient<ICRUD<Nurse>,CRUD<Nurse>>();
            builder.Services.AddTransient<ICRUD<Donor>, CRUD<Donor>>();
            builder.Services.AddTransient<ICRUD<Blood>, CRUD<Blood>>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option => { option.Password.RequireUppercase = false;option.User.RequireUniqueEmail = true;option.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDSZYEFGHIJKLMNOPQ "; })
  .AddEntityFrameworkStores<Data>()
  .AddDefaultTokenProviders();

            var app = builder.Build();

        
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            

         

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Index}");

            app.Run();
        }
    }
}
