using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Models;

namespace Yi.Framework.Model.SeedData
{
    public class DictionarySeed : AbstractSeed<DictionaryEntity>
    {
        public override List<DictionaryEntity> GetSeed()
        {
            return Entitys;
        }
    }
}
