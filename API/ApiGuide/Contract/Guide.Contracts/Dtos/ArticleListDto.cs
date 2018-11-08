using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGuide.Guide.Contracts.Dtos
{
    public class ArticleListDto
    {
        public string Guideid { get; set; }
        public string GuideName { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }

        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
