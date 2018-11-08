
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGuide.Guide.Contracts.Dtos;
using ApiGuide.Guide.Contracts.FB;

namespace ApiGuide.Article.Contracts
{
    public interface IArticleContract
    {
        int Add(ArticleDto dto);
        PageData<ArticleDto> List(ArticleListDto dto);
        ArticleDto Detail(string id);
    }
}
