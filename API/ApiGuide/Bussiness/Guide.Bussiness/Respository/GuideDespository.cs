using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiGuide.Guide.Bussiness.Entities;
using ApiGuide.Guide.Contracts.Dtos;
using ApiGuide.Guide.Contracts.Dtos.FB;
using ApiGuide.Guide.Contracts.FB;
using Dapper;
using MySql.Data.MySqlClient;

namespace ApiGuide.Guide.Bussiness.Respository
{
    public class GuideDespository
    {
        private readonly string constr = "";

        public GuideDespository()
        {
            constr = AppSettingRead.GetConfig("MysqlDB:ConStr");
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        public PageData<TGuides> List(GuideListDto dto)
        {
            StringBuilder condition = new StringBuilder();
            if (!String.IsNullOrEmpty(dto.Name))
            {
                dto.Name = $"%{dto.Name}%";
                condition.Append(" and name like @name");
            }
            if (!String.IsNullOrEmpty(dto.Mobile))
            {
                dto.Mobile = $"%{dto.Mobile}%";
                condition.Append(" and mobile like @mobile");
            }

            string modelQuery = $@"select * from guide  where 1=1 {condition} limit @startindex,@count";
            string countQuery = "select count(1) from guides";
            var mixCondition = new
            {
                startindex = (dto.Page - 1) * dto.Size,
                count = dto.Size
            };
            using (IDbConnection db = new MySqlConnection(constr))
            {
                List<TGuides> list = db.Query<TGuides>(modelQuery, mixCondition).ToList();
                int totalNum = db.Query<int>(countQuery, mixCondition).SingleOrDefault();
                float totlapage =(float)totalNum / dto.Size;
                PageData<TGuides> result = new PageData<TGuides>
                {
                    Items = list,
                    TotalNum = totalNum,
                    TotalPageCount = (int) Math.Ceiling(totlapage)
                };
                return result;
            }

        }

        public TGuides Detail(string id)
        {
            string sql = "select * from Guide where id=@id";
            using (IDbConnection db = new MySqlConnection(constr))
            {
                var data = db.QueryFirstOrDefault<TGuides>(sql, new {id});
                return data;
            }

        }

        public int Insert(TGuides dto)
        {
            string sql= @"INSERT INTO `guide`(`id`, `name`, `mobile`, `certificate`, `headpic`) VALUES (@Id, @Name, @Mobile, @Certificate,@Headpic)";
            
            using (IDbConnection db = new MySqlConnection(constr))
            {
                var data = db.Execute(sql, dto);
                return data;
            }
        }
    }
}
