using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OAuth2Common;

namespace IdentifyPro
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var rsa = new RSACryptoServiceProvider();
            var blockBytes = Convert.FromBase64String(Configuration["SigningCredential"]);
            //从配置文件获取加密证书
            rsa.ImportCspBlob(blockBytes);
            //IdentityServer4授权服务配置
            services.AddIdentityServer()
                .AddSigningCredential(new RsaSecurityKey(rsa))    //设置加密证书
                .AddInMemoryClients(Oauth2Config.GetClients())
                .AddInMemoryIdentityResources(Oauth2Config.GetIdentityResourceResources())


                .AddInMemoryApiResources(Oauth2Config.GeyApiResources())
                //如果是client credentials模式那么就不需要设置验证User了
                .AddResourceOwnerValidator<MyUserValidator>() //User验证接口
                .AddTestUsers(Oauth2Config.GetUsers())    //将固定的Users加入到内存中
                ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Run(async (context) =>
            //{

            //    await context.Response.WriteAsync("TokenEndpoint:sssd,fs:44,dd:Hello World! IdentifyPro");
            //});
            app.UseIdentityServer();
           
        }
    }
}
