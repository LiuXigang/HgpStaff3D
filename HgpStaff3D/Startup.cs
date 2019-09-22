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
using Polly;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Net.Http;

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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "HgpStaff3DApi", Version = "v1" });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "HgpStaff3D.XML");
                c.IncludeXmlComments(xmlPath);
            });
            #region HttpClient
            services.AddHttpClient();
            services.AddHttpClient("github", c =>
            {
                c.BaseAddress = new Uri("https://api.github.com/");
                c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600))
            //重试3次，每次600ms
            );
            services.AddHttpClient("test", c =>
            {
                c.BaseAddress = new Uri("http://localhost:5000/");
                c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            })
    // 熔断
    .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<Exception>().CircuitBreakerAsync(2, TimeSpan.FromSeconds(4), (ex, ts) =>
    {
        Console.WriteLine($"break here {ts.TotalMilliseconds}");
    }, () =>
    {
        Console.WriteLine($"reset here ");
    }))
    // 超时
    .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));

        }

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

        app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("v1/swagger.json", "HgpStaff3D");
        });
    }
}
}
