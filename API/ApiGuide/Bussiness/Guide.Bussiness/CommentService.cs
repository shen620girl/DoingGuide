using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGuide.Comment.Bussiness.Respository;
using ApiGuide.Comment.Contracts;
using ApiGuide.Guide.Bussiness.Entities;
using ApiGuide.Guide.Contracts.Dtos;
using ApiGuide.Guide.Contracts.FB;
using AutoMapper;

namespace ApiGuide.Comment.Bussiness
{
    public class CommentService: ICommentContract
    {
        public readonly CommentDespository _despository;
        public CommentService()
        {
            _despository = new CommentDespository();
       
        }
        public int Add(CommentDto dto)
        {
            var model= Mapper.Map<TComment>(dto);
            var data = _despository.Insert(model);
            return data;
        }
        public PageData<CommentDto> List(CommentListDto dto)
        {
           
            var data =_despository.List(dto);
            //PageData<CommentDto> res = new PageData<CommentDto>
            //{
            //    TotalNum = data.TotalNum,
            //    TotalPageCount = data.TotalPageCount,
            //    Items = Mapper.Map<List<CommentDto>>(data.Items)
            //};
                 var result = Mapper.Map<PageData<CommentDto>>(data);
            return  result;
        }

        public CommentDto Detail(string id)
        {
            var data = _despository.Detail(id);
            return Mapper.Map<CommentDto>(data);
        }
    }
}
