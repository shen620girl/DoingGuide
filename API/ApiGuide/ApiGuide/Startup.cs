using System;
using System.IO;
using System.Security.Cryptography;
using ApiGuide.Guide.Bussiness;
using ApiGuide.Guide.Bussiness.Entities;
using ApiGuide.Guide.Contracts.Dtos;
using ApiGuide.Guide.Contracts.Dtos.FB;
using ApiGuide.Guide.Contracts.FB;
using AutoMapper;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;

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


            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "api1";
                  
                });


            #region 跨域
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:44362")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            #endregion


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<MysqlDB>(this.Configuration.GetSection("MysqlDB"));

            #region swagger ui
            services.AddSwaggerGen(c =>
             {
                 c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                 //  var basePath = PlatformServices.Default.Application.ApplicationBasePath; ; // 获取到应用程序的根路径
                 var basePath = @"D:\MyGithub\DoingGuide\DoingGuide\API\ApiGuide\ApiGuide\";

                 var xmlPath = Path.Combine(basePath, "ApiGuide.xml");

                 c.IncludeXmlComments(xmlPath);
             });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();//添加NLog
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
            app.UseAuthentication();
            app.UseMvc();

            #region swagger ui
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "我的API V1");
            });
            #endregion
        


        }

    }

}

