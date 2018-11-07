using ApiGuide.Guide.Contracts.Dtos.FB;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGuide.Guide.Bussiness
{
    public class AppSettingRead
    {
        private static readonly object objLock = new object();
        private static AppSettingRead instance = null;
        private IConfigurationRoot Config { get; }

        private AppSettingRead()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Config = builder.Build();
        }

        public static AppSettingRead GetInstance()
        {
            if (instance == null)
            {
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new AppSettingRead();
                    }
                }
            }

            return instance;
        }

        public static string GetConfig(string name)
        {
            return GetInstance().Config.GetSection(name).Value;
        }

    }
  

}
