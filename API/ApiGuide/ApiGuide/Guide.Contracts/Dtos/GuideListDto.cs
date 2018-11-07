using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGuide.Guide.Contracts.Dtos
{
    public class GuideListDto
    {
        public string Name { get; set; }
        public string Mobile { get; set; }

        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
