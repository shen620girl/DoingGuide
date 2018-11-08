using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiGuide.Guide.Bussiness;
using ApiGuide.Guide.Bussiness.Entities;
using ApiGuide.Guide.Contracts.Dtos;
using ApiGuide.Guide.Contracts.FB;
using Dapper;
using MySql.Data.MySqlClient;

namespace ApiGuide.Comment.Bussiness.Respository
{
    public class CommentDespository
    {
        private readonly string constr = "";

        public CommentDespository()
        {
            constr = AppSettingRead.GetConfig("MysqlDB:ConStr");
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        public PageData<TComment> List(CommentListDto dto)
        {
            StringBuilder condition = new StringBuilder();
            if (!String.IsNullOrEmpty(dto.Articleid))
            {
               
                condition.Append(" and articleid = @Articleid");
            }
            if (!String.IsNullOrEmpty(dto.Ip))
            {
               
                condition.Append(" and ip = @Ip");
            }
            if (!String.IsNullOrEmpty(dto.Nickname))
            {
                dto.Nickname = $"%{dto.Nickname}%";
                condition.Append(" and Nickname like @Nickname");
            }

            string modelQuery = $@"select  *  from g_Comment   where 1=1 {condition} limit @startindex,@count";
            string countQuery = "select count(1) from g_Comment";
            var mixCondition = new
            {
                startindex = (dto.Page - 1) * dto.Size,
                count = dto.Size
            };
            using (IDbConnection db = new MySqlConnection(constr))
            {
                List<TComment> list = db.Query<TComment>(modelQuery, mixCondition).ToList();
                int totalNum = db.Query<int>(countQuery, mixCondition).SingleOrDefault();
                float totlapage =(float)totalNum / dto.Size;
                PageData<TComment> result = new PageData<TComment>
                {
                    Items = list,
                    TotalNum = totalNum,
                    TotalPageCount = (int) Math.Ceiling(totlapage)
                };
                return result;
            }

        }

        public TComment Detail(string id)
        {
            string sql = "select * from g_Comment where id=@id";
            using (IDbConnection db = new MySqlConnection(constr))
            {
                var data = db.QueryFirstOrDefault<TComment>(sql, new {id});
                return data;
            }

        }

        public int Insert(TComment dto)
        {
            string sql= @"INSERT INTO `guide`.`g_comment`(`id`, `articleid`, `content`, `ip`,`nickname`) VALUES (@Id, @Articleid, @Content, @Ip,@Nickname);";
            
            using (IDbConnection db = new MySqlConnection(constr))
            {
                var data = db.Execute(sql, dto);
                return data;
            }
        }
    }
}
