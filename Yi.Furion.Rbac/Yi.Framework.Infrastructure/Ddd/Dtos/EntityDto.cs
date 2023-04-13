using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;

namespace Yi.Framework.Infrastructure.Ddd.Dtos
{
    [Serializable]
    public abstract class EntityDto<TKey> : EntityDto, IEntityDto<TKey>, IEntityDto
    {
        //
        // 摘要:
        //     Id of the entity.
        public TKey Id { get; set; }

        public override string ToString()
        {
            return $"[DTO: {GetType().Name}] Id = {Id}";
        }
    }

    [Serializable]
    public abstract class EntityDto : IEntityDto
    {
        public override string ToString()
        {
            return "[DTO: " + GetType().Name + "]";
        }
    }
}
