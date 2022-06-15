using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreAppUserIdentity.Models.Context;
using NetCoreAppUserIdentity.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAppUserIdentity
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //MVC Service
            services.AddControllersWithViews();
            //Context
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer("server=YourServerName;database=AppUserIdentityDb;Trusted_Connection=True;"));
            //II.Way Context (netCore6)
            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            //builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString)); => DefaultConnection => aoosetting.json
            //AddIdentity
            //AppUser
            services.AddIdentity<AppUser,IdentityRole>(x=>
            {
                //Þifre yapýlandýrmasý
                x.Password.RequireDigit = false; //Sayý içersinmi (farketmez..)
                x.Password.RequireLowercase = false; //Küçük harf zorunluluðu
                x.Password.RequireUppercase = false; //Büyük harf zorunluluðu
                x.Password.RequireNonAlphanumeric = false; //Özel karakter içersinmi
                x.Password.RequiredLength = 6; //Min 6 karakter..
            }).AddEntityFrameworkStores<AppDbContext>();

            //NetCore6 yapýlandýrma
            //builder.Services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequireDigit = false;
            //    options.Password.RequiredLength = 6;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //});

            //Cookies
            services.ConfigureApplicationCookie(x =>
            {
                x.LogoutPath = new PathString("/Home/SignIn");
                x.AccessDeniedPath = new PathString("/Home/SignIn");//yetkisi yoksa buraya gönder kullanýcýyý..
                x.Cookie = new CookieBuilder()
                {
                    Name = "Yzl3156Cerez"
                };
                x.SlidingExpiration = true; //çerez ömrü
                x.ExpireTimeSpan = TimeSpan.FromMinutes(1);//1 dk xD 
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseStaticFiles();


            app.UseAuthentication();
            app.UseAuthorization();//Authorization iþlemleri için

            app.UseEndpoints(x => {
                x.MapControllerRoute(
                    name: "Areas",
                    pattern: "{area/exist}/{controller=home}/{action=index}/{id?}" //Area Route
                );
                x.MapControllerRoute(
                    name: "default", 
                    pattern: "{Controller=Home}/{Action=Index}/{id?}" //Default Route
                );
            });
        }
    }
}
