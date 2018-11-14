using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("api1/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        public MysqlDB StarInfoConfig;
        //创建容器
     readonly   UnityContainer container = new UnityContainer();
        private IGuideContract _guide;



        public ValuesController(IOptions<MysqlDB> settings, ILogger<ValuesController> logger)
        {
            _logger = logger;
            StarInfoConfig = settings.Value;
            //注册依赖对象
            container.RegisterType<IGuideContract, GuideService>();

            //返回调用者
          _guide=container.Resolve<GuideService>();
           
        }
        // GET api/values
        [HttpGet]
       // [Authorize(AuthenticationSchemes = "token")]
        public ActionResult<IEnumerable<string>> Get()
        {
            //_logger.LogInformation("Index page says hello");
            return new string[] { "value1", "value2",StarInfoConfig.ConStr };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<GuideDto> Get(string id)
        {
            return _guide.Detail(id);
        }
        /// <summary>
        /// guide列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("list"),AllowAnonymous]
        public ActionResult<PageData<GuideDto>> List([FromBody] GuideListDto dto)
        { //执行
            return  _guide.List(dto);
        }
        

        // POST api/values
        [HttpPost]
        public ActionResult<int> Post([FromBody] GuideDto dto)
        {
            return _guide.Add(dto);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
