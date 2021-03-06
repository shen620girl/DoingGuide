﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ApiGuide
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).UseContentRoot(Directory.GetCurrentDirectory()).UseIISIntegration(). Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                    //IdentityServer4的使用需要配置UseUrls
                .UseUrls("http://localhost:5001")
           
                .UseStartup<Startup>();
    }
    //.UseContentRoot(Directory.GetCurrentDirectory())
    //.UseIISIntegration()
    //.UseStartup<Startup>()
    //.Build();
}
