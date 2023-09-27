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
            foreach (var template in templates)
            {
                var handledTempalte = new HandledTemplate();
                handledTempalte.TemplateStr= template.TemplateStr;
                handledTempalte.BuildPath = template.BuildPath;
                foreach (var templateHandler in _templateHandlers)
                {
                    templateHandler.SetTable(tableEntity);
                    handledTempalte = templateHandler.Invoker(handledTempalte.TemplateStr, handledTempalte.BuildPath);
                }
                await BuildToFileAsync(handledTempalte);

            }
        }


        private async Task BuildToFileAsync(HandledTemplate handledTemplate)
        {
            if (!Directory.Exists(Path.GetDirectoryName(handledTemplate.BuildPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(handledTemplate.BuildPath));
            }
            await File.WriteAllTextAsync(handledTemplate.BuildPath,handledTemplate.TemplateStr);
        }


    }

}
