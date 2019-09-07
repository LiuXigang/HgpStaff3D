using HgpStaff3D.Application.IntergrationEvents;
using HgpStaff3D.Application.Queries;
using HgpStaff3D.CapEventHandler;
using HgpStaff3D.Infrastructure;
using HgpStaff3D.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HgpStaff3D
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var defaultConnection = Configuration.GetConnectionString("DefaultConnection");
            #region mvc
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            #endregion
            #region DbContext
            services.AddDbContext<DepartmentContext>(options =>
            {
                options.UseSqlServer(defaultConnection,
                    sql => sql.MigrationsAssembly(typeof(Startup).Assembly.GetName().Name));
            });
            #endregion
            #region Cap
            services.AddCap(options =>
            {
                options.UseSqlServer(defaultConnection)
                    .UseRabbitMQ("localhost")
                    .UseDashboard();
            });
            #endregion
            #region MediatR
            services.AddMediatR(MediatRConfigure.HandlerAssemblyMarkerTypes());
            #endregion
            #region DI

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentQueries, DepartmentQueries>(sp => { return new DepartmentQueries(defaultConnection); });

            //Cap
            services.AddScoped<DepartmentCreaterEventHandler>();

            #endregion
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes => { routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); });
        }
    }
}
