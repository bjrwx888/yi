using Microsoft.AspNetCore.Http;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.Entities;
using Yi.Framework.Ddd.Entities;

namespace Yi.BBS.Domain.Forum.Entities
{
    [SugarTable("Article")]
    public class ArticleEntity : IEntity<long>, ISoftDelete
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        [SugarColumn(Length = 999999)]
        public string Content { get; set; }
        public string Name { get; set; }


        public long DiscussId { get; set; }

        public long ParentId { get; set; }

        [SugarColumn(IsIgnore =true)]

        public List<ArticleEntity> Children { get; set; }
    }

    public static class ArticleEntityExtensions
    {
        /// <summary>
        /// 平铺自己
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ArticleEntity> Tile(this List<ArticleEntity> entities)
        {
            if(entities is null)return new List<ArticleEntity>();
            var result = new List<ArticleEntity>();
            return StartRecursion(entities, result);
        }

        private static List<ArticleEntity> StartRecursion(List<ArticleEntity> entities, List<ArticleEntity> result)
        {
            foreach (var entity in entities)
            {
                result.Add(entity);
                if (entity.Children is not null && entity.Children.Where(x => x.IsDeleted == false).Count() > 0)
                {
                    StartRecursion(entity.Children, result);
                }
            }
            return result;
        }

    }
}
