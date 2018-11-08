using System;

using ApiGuide.Article.Bussiness;
using ApiGuide.Article.Contracts;
using ApiGuide.Guide.Bussiness.Entities;
using ApiGuide.Guide.Contracts.Dtos;
using ApiGuide.Guide.Contracts.FB;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using UnitTestGuide.Common;
using Unity;

namespace UnitTestArticle
{
    [TestClass]
    public class ArticleTest1: CwHelper
    {
        //创建容器
        readonly UnityContainer container = new UnityContainer();
        private IArticleContract _Article;

        public ArticleTest1()
        {
            //注册依赖对象
            container.RegisterType<IArticleContract, ArticleService>();

            //返回调用者
            _Article = container.Resolve<ArticleService>();

            Mapper.Initialize(x => x.CreateMap<PageData<TArticle>, PageData<ArticleDto>>());
        }
        [TestMethod]
        public void AddTest()
        {
            var dto = new ArticleDto
            {
                Id = Guid.NewGuid().ToString().Remove(2,7),
                Guideid = "sdf4444",
                Title="cicxxxx",
                Content = "sdfsdfssssd"
              
            };
            Print(_Article.Add(dto));
        }

        [TestMethod]
        public void ListTest()
        {
            Print(_Article.List(new ArticleListDto{Page=1,Size=6}));
            //Print(new {d1="4453",dd=345,shen="sdfsd"});
        }

        [TestMethod]
        public void DetailTest()
        {
            string id = "c2afc8-4a5b-b1fe-366c17e8c0e5";
            Print(_Article.Detail(id));
        }
  
    }
}
