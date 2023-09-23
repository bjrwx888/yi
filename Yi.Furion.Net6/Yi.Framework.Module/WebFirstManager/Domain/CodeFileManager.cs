using Furion.DependencyInjection;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Module.WebFirstManager.Entities;
using Yi.Framework.Module.WebFirstManager.Handler;

namespace Yi.Framework.Module.WebFirstManager.Domain
{
    /// <summary>
    /// 代码文件领域服务,与代码文件生成相关，web to code
    /// </summary>
    public class CodeFileManager : ITransient
    {
        private IEnumerable<ITemplateHandler> _templateHandlers;
        private IRepository<TemplateEntity> _repository;
        private IRepository<FieldEntity> _fieldRepository;
        public CodeFileManager(IEnumerable<ITemplateHandler> templateHandlers, IRepository<FieldEntity> fieldRepository, IRepository<TemplateEntity> repository)
        {
            _templateHandlers = templateHandlers;
            _repository = repository;
            _fieldRepository = fieldRepository;
        }
        public async Task BuildWebToCodeAsync(TableAggregateRoot tableEntity)
        {
            var templates = await _repository.GetListAsync();
            var fields = await _fieldRepository.GetListAsync();
            foreach (var template in templates)
            {
                string templateStr = template.TemplateStr;
                foreach (var templateHandler in _templateHandlers)
                {
                    templateHandler.SetTable(tableEntity);
                    templateStr = templateHandler.Invoker(templateStr);
                }

                await BuildToFileAsync(templateStr, template);
            }
        }


        private async Task BuildToFileAsync(string str, TemplateEntity templateEntity)
        {

            //await File.WriteAllTextAsync(str, templateEntity.BuildPath);
        }


    }

}
