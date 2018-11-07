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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Unity;

namespace ApiGuide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public MysqlDB StarInfoConfig;
        //创建容器
     readonly   UnityContainer container = new UnityContainer();
        private IGuideContract _guide;



        public ValuesController(IOptions<MysqlDB> settings)
        {
            StarInfoConfig = settings.Value;
            //注册依赖对象
            container.RegisterType<IGuideContract, GuideService>();

            //返回调用者
          _guide=container.Resolve<GuideService>();
           
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
           
            return new string[] { "value1", "value2",StarInfoConfig.ConStr };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
        // GET api/values/5
        [HttpGet("yan")]
        public ActionResult<PageData<GuideDto>> GetYan()
        { //执行
            return  _guide.List(new GuideListDto {Page = 1, Size = 2});
        }
        

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
