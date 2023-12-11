using Volo.Abp.Uow;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.SqlSugarCore.Uow
{
    public class SqlSugarTransactionApi : ITransactionApi, ISupportsRollback
    {
        private ISqlSugarDbContext _sqlsugarDbContext;

        public SqlSugarTransactionApi(ISqlSugarDbContext sqlsugarDbContext)
        {
            _sqlsugarDbContext = sqlsugarDbContext;

        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {


            await Console.Out.WriteLineAsync("事务提交");

            Console.WriteLine(_sqlsugarDbContext.SqlSugarClient.ContextID + "---------------");
            await _sqlsugarDbContext.SqlSugarClient.Ado.CommitTranAsync();
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose");
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            await Console.Out.WriteLineAsync("事务回滚");
            Console.WriteLine(_sqlsugarDbContext.SqlSugarClient.ContextID);
            await _sqlsugarDbContext.SqlSugarClient.Ado.RollbackTranAsync();
        }
    }
}
