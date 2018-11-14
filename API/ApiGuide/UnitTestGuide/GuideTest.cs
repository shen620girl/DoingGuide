using System;
using System.Security.Cryptography;
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

            Mapper.Initialize(x => x.CreateMap<PageData<TGuide>, PageData<GuideDto>>());
        }
        [TestMethod]
        public void AddTest()
        {
            var dto = new GuideDto
            {
                Id = Guid.NewGuid().ToString().Remove(2,7),
                Name = "jjjjjjjjjjjj",
                Mobile = "15988888888",
                Certificate = "345334.url",
                Headpic = "http://wwes.ssr"
            };
            Print(_guide.Add(dto));
        }

        [TestMethod]
        public void ListTest()
        {
            Print(_guide.List(new GuideListDto{Page=1,Size=6}));
            //Print(new {d1="4453",dd=345,shen="sdfsd"});
        }

        [TestMethod]
        public void PrivateKeyTest()
        {
            using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(2048))
                             {
                                 //Console.WriteLine(Convert.ToBase64String(provider.ExportCspBlob(false)));   //PublicKey
                            //    Console.WriteLine(Convert.ToBase64String(provider.ExportCspBlob(true)));    //PrivateKey
                                 Print(Convert.ToBase64String(provider.ExportCspBlob(false)));
                                 Print("cesiceshi");
                                 Print(Convert.ToBase64String(provider.ExportCspBlob(true)));
            }
        }
        [TestMethod]
        public void DetailTest()
        {
            string id = "1111111111111";
            Print(_guide.Detail(id));
        }
        [TestMethod]
        public void TestTotalPage()
        {
            double dd = (double)2 / 3;
            float dds = (float)2 / 3;
            Print($"{dd}...sss{dds}");
            //Print(new {d1="4453",dd=345,shen="sdfsd"});
        }
    }
}
