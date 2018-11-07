
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGuide.Guide.Contracts.Dtos;
using ApiGuide.Guide.Contracts.FB;

namespace ApiGuide.Guide.Contracts
{
    public interface IGuideContract
    {
        int Add(GuideDto dto);
        PageData<GuideDto> List(GuideListDto dto);
        GuideDto Detail(string id);
    }
}
