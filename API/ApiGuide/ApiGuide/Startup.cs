using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using ApiGuide.Guide.Bussiness.Entities;
using ApiGuide.Guide.Contracts.Dtos;
using ApiGuide.Guide.Contracts.Dtos.FB;
using ApiGuide.Guide.Contracts.FB;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace ApiGuide
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<MysqlDB>(this.Configuration.GetSection("MysqlDB"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
              //  var basePath = PlatformServices.Default.Application.ApplicationBasePath; ; // 获取到应用程序的根路径
                var basePath = @"D:\MyGithub\DoingGuide\DoingGuide\API\ApiGuide\ApiGuide\";
              //  var basePath = PlatformServices.Default.Application.ApplicationBasePath;

                var xmlPath = Path.Combine(basePath, "ApiGuide.xml");

               // var xmlPath1 = Path.Combine(basePath, "MyDemo.Model.xml");

                c.IncludeXmlComments(xmlPath);
              //  c.IncludeXmlComments(xmlPath1);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Mapper.Initialize(x => x.CreateMap<PageData<TGuide>, PageData<GuideDto>>());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "我的API V1");
            });
        }
    }
}
