using System;
using System.IO;
using System.Security.Cryptography;
using ApiGuide.Guide.Bussiness;
using ApiGuide.Guide.Bussiness.Entities;
using ApiGuide.Guide.Contracts.Dtos;
using ApiGuide.Guide.Contracts.Dtos.FB;
using ApiGuide.Guide.Contracts.FB;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using OAuth2Common;
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

        
            #region identify server 4
            //RSA：证书长度2048以上，否则抛异常
            //配置AccessToken的加密证书
            var rsa = new RSACryptoServiceProvider();
            var blockBytes = Convert.FromBase64String(Configuration["SigningCredential"]);
            //从配置文件获取加密证书
            //  rsa.ImportCspBlob(blockBytes);
            //IdentityServer4授权服务配置
            services.AddIdentityServer()
                .AddSigningCredential(new RsaSecurityKey(rsa))    //设置加密证书
                                                                 //.AddTemporarySigningCredential()    //测试的时候可使用临时的证书

                //    .AddInMemoryScopes(OAuth2Config.GetScopes())

                .AddInMemoryClients(OAuth2Config.GetClients())
                .AddInMemoryApiResources(OAuth2Config.GeyApiResources())
                //如果是client credentials模式那么就不需要设置验证User了
                .AddResourceOwnerValidator<MyUserValidator>() //User验证接口
                .AddTestUsers(OAuth2Config.GetUsers())    //将固定的Users加入到内存中
                ;

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
            app.UseMvc();

            #region swagger ui
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "我的API V1");
            });
            #endregion
            //使用IdentityServer4的授权服务
            app.UseIdentityServer();


        }

    }

}

