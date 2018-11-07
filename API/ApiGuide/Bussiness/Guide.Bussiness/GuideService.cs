using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGuide.Guide.Bussiness.Entities;
using ApiGuide.Guide.Bussiness.Respository;
using ApiGuide.Guide.Contracts;
using ApiGuide.Guide.Contracts.Dtos;
using ApiGuide.Guide.Contracts.FB;
using AutoMapper;

namespace ApiGuide.Guide.Bussiness
{
    public class GuideService: IGuideContract
    {
        public readonly GuideDespository _despository;
        public GuideService()
        {
            _despository = new GuideDespository();
       
        }
        public PageData<GuideDto> List(GuideListDto dto)
        {
           
            var data =_despository.List(dto);
            //PageData<GuideDto> res = new PageData<GuideDto>
            //{
            //    TotalNum = data.TotalNum,
            //    TotalPageCount = data.TotalPageCount,
            //    Items = Mapper.Map<List<GuideDto>>(data.Items)
            //};
                 var result = Mapper.Map<PageData<GuideDto>>(data);
            return  result;
        } 
    }
}
