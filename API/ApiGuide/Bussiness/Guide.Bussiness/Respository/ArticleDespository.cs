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

namespace ApiGuide.Article.Bussiness.Respository
{
    public class ArticleDespository
    {
        private readonly string constr = "";

        public ArticleDespository()
        {
            constr = AppSettingRead.GetConfig("MysqlDB:ConStr");
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        public PageData<TArticle> List(ArticleListDto dto)
        {
            StringBuilder condition = new StringBuilder();
            if (!String.IsNullOrEmpty(dto.GuideName))
            {
                dto.GuideName = $"%{dto.GuideName}%";
                condition.Append(" and b.name like @GuideName");
            }
            if (!String.IsNullOrEmpty(dto.Content))
            {
                dto.Content = $"%{dto.Content}%";
                condition.Append(" and a.Content like @Content");
            }
            if (!String.IsNullOrEmpty(dto.Title))
            {
                dto.Title = $"%{dto.Title}%";
                condition.Append(" and a.title like @Title");
            }

            string modelQuery = $@"select  a.* from g_Article as a left join g_guide as b on a.Guideid=b.id  where 1=1 {condition} limit @startindex,@count";
            string countQuery = "select count(1) from g_Article";
            var mixCondition = new
            {
                startindex = (dto.Page - 1) * dto.Size,
                count = dto.Size
            };
            using (IDbConnection db = new MySqlConnection(constr))
            {
                List<TArticle> list = db.Query<TArticle>(modelQuery, mixCondition).ToList();
                int totalNum = db.Query<int>(countQuery, mixCondition).SingleOrDefault();
                float totlapage =(float)totalNum / dto.Size;
                PageData<TArticle> result = new PageData<TArticle>
                {
                    Items = list,
                    TotalNum = totalNum,
                    TotalPageCount = (int) Math.Ceiling(totlapage)
                };
                return result;
            }

        }

        public TArticle Detail(string id)
        {
            string sql = "select * from g_Article where id=@id";
            using (IDbConnection db = new MySqlConnection(constr))
            {
                var data = db.QueryFirstOrDefault<TArticle>(sql, new {id});
                return data;
            }

        }

        public int Insert(TArticle dto)
        {
            string sql= @"INSERT INTO `guide`.`g_article`(`id`, `title`, `guideid`, `content`) VALUES (@Id, @Guideid, @Content, @Title);
";
            
            using (IDbConnection db = new MySqlConnection(constr))
            {
                var data = db.Execute(sql, dto);
                return data;
            }
        }
    }
}
