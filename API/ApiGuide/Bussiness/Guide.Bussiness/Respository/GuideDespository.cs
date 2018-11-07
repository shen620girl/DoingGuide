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

        //public List<TGuides> List()
        //{

        //    using (IDbConnection db = new MySqlConnection(constr))
        //    {



        //        return db.Query<TGuides>("Select * From Guides").ToList();
        //    }
        //}

        public PageData<TGuides> List(GuideListDto dto)
        {

            using (IDbConnection db = new MySqlConnection(constr))
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

                String modelQuery = $@"select * from guides  where 1=1 {condition} limit @startindex,@count";
                string countQuery = "select count(1) from guides";
                var mixCondition = new
                {
                    startindex = (dto.Page - 1) * dto.Size,
                    count = dto.Size
                };
                List<TGuides> list = db.Query<TGuides>(modelQuery, mixCondition).ToList();
                int totalNum = db.Query<Int32>(countQuery, mixCondition).SingleOrDefault<Int32>();
                decimal totlapage = totalNum / dto.Size;
                PageData<TGuides> result = new PageData<TGuides>
                {
                    Items = list,
                    TotalNum = totalNum,
                    TotalPageCount = (int) Math.Ceiling(totlapage)
                };
                return result;
            }

        }
    }
}
