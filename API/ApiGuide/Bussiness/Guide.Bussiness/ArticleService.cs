using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGuide.Article.Bussiness.Respository;
using ApiGuide.Article.Contracts;
using ApiGuide.Guide.Bussiness.Entities;
using ApiGuide.Guide.Contracts.Dtos;
using ApiGuide.Guide.Contracts.FB;
using AutoMapper;

namespace ApiGuide.Article.Bussiness
{
    public class ArticleService: IArticleContract
    {
        public readonly ArticleDespository _despository;
        public ArticleService()
        {
            _despository = new ArticleDespository();
       
        }
        public int Add(ArticleDto dto)
        {
            var model= Mapper.Map<TArticle>(dto);
            var data = _despository.Insert(model);
            return data;
        }
        public PageData<ArticleDto> List(ArticleListDto dto)
        {
           
            var data =_despository.List(dto);
            //PageData<ArticleDto> res = new PageData<ArticleDto>
            //{
            //    TotalNum = data.TotalNum,
            //    TotalPageCount = data.TotalPageCount,
            //    Items = Mapper.Map<List<ArticleDto>>(data.Items)
            //};
                 var result = Mapper.Map<PageData<ArticleDto>>(data);
            return  result;
        }

        public ArticleDto Detail(string id)
        {
            var data = _despository.Detail(id);
            return Mapper.Map<ArticleDto>(data);
        }
    }
}
