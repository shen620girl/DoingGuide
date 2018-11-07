using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGuide.Guide.Contracts.Dtos
{
    public class GuideDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Certificate { get; set; }
        public string Headpic { get; set; }
    }
}
