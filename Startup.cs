using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ElmahCore;
using ElmahCore.Mvc;
using MagicBoxAdminPortal.generalrepositoryinterface.CreateHashByKey;
using MagicBoxSupport.Repository.Interfaces.RetailerMgt;
using MagicBoxSupport.Repository.Repositories.RetailerMgtRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;



namespace MagicBoxAdminPortal
{
    public class Startup
    {
        public static IConfiguration StaticConfig { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfig = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

           
          //  services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
           
            //services.Configure<IISServerOptions>(options =>
            //{
            //    options.MaxRequestBodySize = Int64.MaxValue;
            //});

          
           


            //Accept xml request type
            //  services.AddControllers().AddXmlDataContractSerializerFormatters();
            //swagger 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MagicBoxAdminPortal", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //   c.IncludeXmlComments(xmlPath);
            });


            //elmah




            services.AddControllers();

            //services.AddControllersWithViews().
            //     AddJsonOptions(options =>
            //     {
            //         options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            //         options.JsonSerializerOptions.PropertyNamingPolicy = null;
            //     });

            // configure strongly typed settings objects
           


            services.AddSingleton<IConfiguration>(Configuration);

            //// configure jwt authentication



                  services.AddTransient<IRetailerMgtRepository, RetailerMgtRepository>();
                  services.AddTransient<ICreateHashByKey, CreateHashByKey>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MagicBoxAdminPortal V1");
            });

            app.UseSwaggerUI(s =>
            {
                s.RoutePrefix = "help";
                s.SwaggerEndpoint("../swagger/v1/swagger.json", "MagicBoxAdminPortal");
                s.InjectStylesheet("../css/swagger.min.css");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //use elmah ui fixure
            
            //elmah
           // app.UseElmah();

            app.UseRouting();

            // global cors policy
            
           
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


        }
    }
}
