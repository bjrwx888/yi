using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion.DependencyInjection;

namespace Yi.Framework.Infrastructure.Data.DataSeeds
{

    public class DataSeedExecuteHandler : ISingleton
    {
        private IEnumerable<IDataSeed> _dataSeeds;
        public DataSeedExecuteHandler(IEnumerable<IDataSeed> dataSeeds)
        {
            _dataSeeds = dataSeeds;
        }

        public async Task Invoker()
        {
            foreach (var dataSeed in _dataSeeds)
            {
                await dataSeed.InvokerAsync();
            }
        }
    }
}
