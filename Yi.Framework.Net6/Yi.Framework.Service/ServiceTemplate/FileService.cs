using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class FileService : BaseService<FileEntity>, IFileService
    {
        public FileService(IRepository<FileEntity> repository) : base(repository)
        {
        }
    }
}
