using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.DataSeeds;
using Yi.Framework.Ddd.Repositories;
using Yi.Template.Domain.School.Entities;

namespace Yi.Template.Domain.School.DataSeeds
{
    [AppService(typeof(IDataSeed))]
    public class StudentDataSeed : AbstractDataSeed<StudentEntity>
    {
        public StudentDataSeed(IRepository<StudentEntity> repository) : base(repository)
        {
        }

        public override List<StudentEntity> GetSeedData()
        {
            return new List<StudentEntity>() { new StudentEntity { Id = SnowflakeHelper.NextId, Name = "你好", Phone = "123", Height = 188, IsDeleted = false } ,
            new StudentEntity { Id = SnowflakeHelper.NextId, Name = "你好1", Phone = "123", Height = 188, IsDeleted = false },
            new StudentEntity { Id = SnowflakeHelper.NextId, Name = "你好2", Phone = "123", Height = 188, IsDeleted = false }
            };
        }
    }
}
