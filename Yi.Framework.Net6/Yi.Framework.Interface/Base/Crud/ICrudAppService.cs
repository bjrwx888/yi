using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Interface.Base.Crud
{
    public interface ICrudAppService<TEntityDto, in TKey>
           : ICrudAppService<TEntityDto, TKey, TEntityDto>
    {

    }

    public interface ICrudAppService<TEntityDto, in TKey, in TCreateInput>
        : ICrudAppService<TEntityDto, TKey, TCreateInput, TCreateInput>
    {

    }

    public interface ICrudAppService<TEntityDto, in TKey, in TCreateInput, in TUpdateInput>
        : ICrudAppService<TEntityDto, TEntityDto, TKey, TCreateInput, TUpdateInput>
    {

    }

    public interface ICrudAppService<TGetOutputDto, TGetListOutputDto, in TKey, in TCreateInput, in TUpdateInput>
        : IReadOnlyAppService<TGetOutputDto, TGetListOutputDto, TKey>,
            ICreateUpdateAppService<TGetOutputDto, TKey, TCreateInput, TUpdateInput>,
            IDeleteAppService<TKey>
    {

    }
}
