using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Services;
using Yi.Abp.Tool.Application.Contracts.Dtos;

namespace Yi.Abp.Tool.Application.Contracts
{
    public interface ITemplateGenService: IApplicationService
    {
        Task<IActionResult> CreateModuleAsync(TemplateGenCreateInputDto moduleCreateInputDto);
        Task<IActionResult> CreateProjectAsync(TemplateGenCreateInputDto moduleCreateInputDto);
    }
}
