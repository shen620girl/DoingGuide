using ApiGuide.Guide.Bussiness.Entities;
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
        PageData<GuideDto> List(GuideListDto dto);
    }
}
