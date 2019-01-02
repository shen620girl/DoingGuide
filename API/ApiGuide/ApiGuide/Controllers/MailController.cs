using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGuide.Common;
using ApiGuide.Guide.Bussiness;
using ApiGuide.Guide.Bussiness.Entities;
using ApiGuide.Guide.Contracts;
using ApiGuide.Guide.Contracts.Dtos;
using ApiGuide.Guide.Contracts.Dtos.FB;
using ApiGuide.Guide.Contracts.FB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Unity;

namespace ApiGuide.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly ILogger<GuideController> _logger;
        public MysqlDB StarInfoConfig;
        //创建容器
     readonly   UnityContainer container = new UnityContainer();
        private IGuideContract _guide;

        public MailController(IOptions<MysqlDB> settings, ILogger<GuideController> logger)
        {
            _logger = logger;
            StarInfoConfig = settings.Value;
            //注册依赖对象  //返回调用者
            container.RegisterType<IGuideContract, GuideService>();
          _guide=container.Resolve<GuideService>();

           
        }


        [HttpPost]
        public void Post( string subject, string emailaddress)
        {
            MailHelper.SecondTry(subject, emailaddress);

        }

    }
}
