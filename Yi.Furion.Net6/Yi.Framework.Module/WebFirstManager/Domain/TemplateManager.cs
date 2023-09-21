using Furion.DependencyInjection;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Module.WebFirstManager.Entities;
using Yi.Framework.Module.WebFirstManager.Handler;

namespace Yi.Framework.Module.WebFirstManager.Domain
{
    /// <summary>
    /// 模板领域服务
    /// </summary>
    public class TemplateManager : ITransient
    {
        private IEnumerable<ITemplateHandler> _templateHandlers;
        private IRepository<TemplateEntity> _repository;
        private IRepository<FieldEntity> _fieldRepository;
        public TemplateManager(IEnumerable<ITemplateHandler> templateHandlers, IRepository<FieldEntity> fieldRepository, IRepository<TemplateEntity> repository)
        {
            _templateHandlers = templateHandlers;
            _repository = repository;
            _fieldRepository = fieldRepository;
        }
        public async Task HandlerAsync(TableEntity tableEntity)
        {
            var templates = await _repository.GetListAsync();
            var fields = await _fieldRepository.GetListAsync();
            foreach (var template in templates)
            {
                string templateStr = template.TemplateStr;
                foreach (var templateHandler in _templateHandlers)
                {
                    templateHandler.SetTable(tableEntity);
                    templateHandler.SetFields(fields);
                    templateStr = templateHandler.Invoker(templateStr);
                }

                await BuildToFileAsync(templateStr, template);
            }
        }


        private async Task BuildToFileAsync(string str, TemplateEntity templateEntity)
        {
            await File.WriteAllTextAsync(str, templateEntity.BuildPath);
        }
    }

}
