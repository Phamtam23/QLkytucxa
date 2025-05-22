using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quanlykytucxa.Models;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Quanlykytucxa.Services.Momo;
namespace Quanlykytucxa
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //--configure kết nói api momo
            services.Configure<MomoOptionModel>(Configuration.GetSection("MoMoAPI"));
            services.AddScoped<IMomoservice, Momoservice>();

            services.AddControllersWithViews();
            services.AddDbContext<QuanLyKTXContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnections")));
            services.AddIdentity<SinhVien, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            }).AddEntityFrameworkStores<QuanLyKTXContext>()
                .AddDefaultTokenProviders();
          

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Route cho Area Admin (ưu tiên trước)
                endpoints.MapControllerRoute(
                    name: "AdminArea",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}",
                    defaults: new { area = "Admin" });

                // Route mặc định cho các controller không thuộc Area
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"


                );
            });

        }
    }
}
