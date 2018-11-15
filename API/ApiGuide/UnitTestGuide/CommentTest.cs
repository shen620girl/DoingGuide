using System;

using ApiGuide.Comment.Bussiness;
using ApiGuide.Comment.Contracts;
using ApiGuide.Common;
using ApiGuide.Guide.Bussiness.Entities;
using ApiGuide.Guide.Contracts.Dtos;
using ApiGuide.Guide.Contracts.FB;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using UnitTestGuide.Common;
using Unity;

namespace UnitTestComment
{
    [TestClass]
    public class CommentTest1: CwHelper
    {
        //创建容器
        readonly UnityContainer container = new UnityContainer();
        private ICommentContract _Comment;

        public CommentTest1()
        {
            //注册依赖对象
            container.RegisterType<ICommentContract, CommentService>();

            //返回调用者
            _Comment = container.Resolve<CommentService>();

            Mapper.Initialize(x => x.CreateMap<PageData<TComment>, PageData<CommentDto>>());
        }
        [TestMethod]
        public void AddTest()
        {
            var dto = new CommentDto
            {
                Id = Guid.NewGuid().ToString().Remove(2,7),
                Content = "content1107001",
                Nickname= "nikename001",
                Ip = "198.2563.255.3"
              
            };
            Print(_Comment.Add(dto));
        }

        [TestMethod]
        public void ListTest()
        {
           // MailHelper.SecondTry();
            Print(_Comment.List(new CommentListDto{Page=1,Size=6}));
            //Print(new {d1="4453",dd=345,shen="sdfsd"});
        }

        [TestMethod]
        public void DetailTest()
        {
            string id = "c2afc8-4a5b-b1fe-366c17e8c0e5";
            Print(_Comment.Detail(id));
        }
  
    }
}
