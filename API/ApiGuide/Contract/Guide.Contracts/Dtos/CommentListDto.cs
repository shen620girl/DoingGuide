using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGuide.Guide.Contracts.Dtos
{
    public class CommentListDto
    {
        public string Articleid { get; set; }
        public string Content { get; set; }
        public string Ip { get; set; }
        public string Nickname { get; set; }

        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
