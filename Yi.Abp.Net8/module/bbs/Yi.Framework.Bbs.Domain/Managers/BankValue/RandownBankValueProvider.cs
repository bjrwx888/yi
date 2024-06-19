using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Yi.Framework.Bbs.Domain.Managers.BankValue
{
    [Dependency(ReplaceServices = true)]
    internal class RandownBankValueProvider : IBankValueProvider, ITransientDependency
    {
        public Task<decimal> GetValueAsync()
        {
            var currentNumber = new Random().Next(800, 1200);
            return Task.FromResult((decimal)currentNumber);
        }
    }
}
