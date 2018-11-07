using System;
using ApiGuide.Guide.Bussiness;
using ApiGuide.Guide.Bussiness.Entities;
using ApiGuide.Guide.Contracts;
using ApiGuide.Guide.Contracts.Dtos;
using ApiGuide.Guide.Contracts.FB;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestGuide.Common;
using Unity;

namespace UnitTestGuide
{
    [TestClass]
    public class UnitTest1: CwHelper
    {
        //创建容器
        readonly UnityContainer container = new UnityContainer();
        private IGuideContract _guide;

        public UnitTest1()
        {
            //注册依赖对象
            container.RegisterType<IGuideContract, GuideService>();

            //返回调用者
            _guide = container.Resolve<GuideService>();

            Mapper.Initialize(x => x.CreateMap<PageData<TGuides>, PageData<GuideDto>>());
        }


        [TestMethod]
        public void TestMethod1()
        {
            Print(_guide.List(new GuideListDto{Page=1,Size=1}));
            //Print(new {d1="4453",dd=345,shen="sdfsd"});
        }
    }
}
