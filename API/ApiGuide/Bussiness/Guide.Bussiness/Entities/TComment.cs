using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGuide.Guide.Bussiness.Entities
{
    public class TComment
    {
        public string Id { get; set; }
        public string Articleid { get; set; }
        public string Content { get; set; }
        public string Ip { get; set; }
        public string Nickname { get; set; }
    }
}
