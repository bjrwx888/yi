using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yi.Abp.Tool.Application.Contracts.Dtos;

namespace Yi.Abp.Tool.Application.Contracts
{
    public interface ITemplateGenService
    {
        Task<IActionResult> CreateModuleAsync(TemplateGenCreateInputDto moduleCreateInputDto);
        Task<IActionResult> CreateProjectAsync(TemplateGenCreateInputDto moduleCreateInputDto);
    }
}
