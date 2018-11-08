
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGuide.Guide.Contracts.Dtos;
using ApiGuide.Guide.Contracts.FB;

namespace ApiGuide.Comment.Contracts
{
    public interface ICommentContract
    {
        int Add(CommentDto dto);
        PageData<CommentDto> List(CommentListDto dto);
        CommentDto Detail(string id);
    }
}
