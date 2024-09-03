using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Volo.Abp.Application.Services;
using Volo.Abp.Uow;
using Yi.Framework.GoView.Application.Contracts.Dtos;
using Yi.Framework.GoView.Domain.Entities;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.GoView.Application.Services
{
    /// <summary>
    /// GoView项目服务
    /// </summary>
    public class GoViewProjectService : ApplicationService
    {
        private readonly ISqlSugarRepository<GoViewProjectEntity, long> _goViewProjectRep;
        private readonly ISqlSugarRepository<GoViewProjectDataEntity, long> _goViewProjectDataRep;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="goViewProjectRep"></param>
        /// <param name="goViewProjectDataRep"></param>
        public GoViewProjectService(ISqlSugarRepository<GoViewProjectEntity, long> goViewProjectRep, ISqlSugarRepository<GoViewProjectDataEntity, long> goViewProjectDataRep)
        {
            _goViewProjectRep = goViewProjectRep;
            _goViewProjectDataRep = goViewProjectDataRep;
        }

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("go-view-system/list")]
        public async Task<List<GoViewProjectItemOutput>> GetList([FromQuery] int page = 1, [FromQuery] int limit = 12)
        {
            var res = await _goViewProjectRep._DbQueryable
                .Where(p => !p.IsDeleted)
                .Select(u => new GoViewProjectItemOutput(), true)
                .ToPageListAsync(page, limit);
            return res;
        }

        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("go-view-system/create")]
        public async Task<GoViewProjectCreateOutput> Create(GoViewProjectCreateInput input)
        {
            var project = await _goViewProjectRep.InsertReturnEntityAsync(input.Adapt<GoViewProjectEntity>());
            return new GoViewProjectCreateOutput
            {
                Id = project.Id.ToString()
            };
        }

        /// <summary>
        /// 修改项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("go-view-system/edit")]
        public async Task Edit(GoViewProjectEditInput input)
        {
            await (await _goViewProjectRep.AsUpdateable(input.Adapt<GoViewProjectEntity>())).IgnoreColumns(true).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        [UnitOfWork]
        [HttpDelete("go-view-system/delete")]
        public async Task Delete([FromQuery] string ids)
        {
            var idList = ids.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(u => Convert.ToInt64(u)).ToList();
            await _goViewProjectRep.DeleteAsync(u => idList.Contains(u.Id));
            await _goViewProjectDataRep.DeleteAsync(u => idList.Contains(u.Id));
        }

        /// <summary>
        /// 修改发布状态
        /// </summary>
        [HttpPut("go-view-system/publish")]
        public async Task Publish(GoViewProjectPublishInput input)
        {
            await _goViewProjectRep.UpdateAsync(u => new GoViewProjectEntity { State = input.State }, u => u.Id == input.Id);
        }

        /// <summary>
        /// 获取项目数据
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("go-view-system/getData")]
        public async Task<GoViewProjectDetailOutput?> GetData([FromQuery] long projectId)
        {
            var projectData = await _goViewProjectDataRep.GetFirstAsync(u => u.Id == projectId && !u.IsDeleted);
            if (projectData == null) return null;

            var project = await _goViewProjectRep.GetFirstAsync(u => u.Id == projectId && !u.IsDeleted);
            var projectDetail = project.Adapt<GoViewProjectDetailOutput>();
            projectDetail.Content = projectData.Content ?? string.Empty;

            return projectDetail;
        }

        /// <summary>
        /// 保存项目数据
        /// </summary>
        [HttpPost("go-view-system/save/data")]
        public async Task SaveData([FromForm] GoViewProjectSaveDataInput input)
        {
            if (await _goViewProjectDataRep.IsAnyAsync(u => u.Id == input.ProjectId))
            {
                await _goViewProjectDataRep.UpdateAsync
                    (u => new GoViewProjectDataEntity
                    {
                        Content = input.Content
                    }, u => u.Id == input.ProjectId);
            }
            else
            {
                await _goViewProjectDataRep.InsertAsync(new GoViewProjectDataEntity
                {
                    Id = input.ProjectId,
                    Content = input.Content,
                });
            }
        }

        /// <summary>
        /// 上传预览图
        /// </summary>
        [HttpPost("go-view-system/upload")]
        public async Task<GoViewProjectUploadOutput> Upload(IFormFile @object)
        {
            /*
             * 前端逻辑（useSync.hook.ts 的 dataSyncUpdate 方法）：
             * 如果 FileUrl 不为空，使用 FileUrl
             * 否则使用 GetOssInfo 接口获取到的 BucketUrl 和 FileName 进行拼接
             */

            // 文件名格式示例 13414795568325_index_preview.png
            var fileNameSplit = @object.FileName.Split('_');
            var idStr = fileNameSplit[0];
            if (!long.TryParse(idStr, out var id)) return new GoViewProjectUploadOutput();

            // 将预览图转换成 Base64
            var ms = new MemoryStream();
            await @object.CopyToAsync(ms);
            var base64Image = Convert.ToBase64String(ms.ToArray());

            // 保存
            if (await _goViewProjectDataRep.IsAnyAsync(u => u.Id == id))
            {
                await _goViewProjectDataRep.UpdateAsync
                    (u => new GoViewProjectDataEntity
                    {
                        IndexImageData = base64Image
                    }, u => u.Id == id);
            }
            else
            {
                await _goViewProjectDataRep.InsertAsync(new GoViewProjectDataEntity
                {
                    Id = id,
                    IndexImageData = base64Image,
                });
            }

            var output = new GoViewProjectUploadOutput
            {
                Id = id,
                BucketName = null,
                CreateTime = null,
                CreateUserId = null,
                FileName = null,
                FileSize = 0,
                FileSuffix = "png",
                FileUrl = $"api/app/go-view-system/getIndexImage/{id}",
                UpdateTime = null,
                UpdateUserId = null
            };

            return output;
        }

        /// <summary>
        /// 获取预览图
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("go-view-system/getIndexImage/{id}")]
        public async Task<IActionResult> GetIndexImage(string id)
        {
            var projectData = await _goViewProjectDataRep.GetByIdAsync(id);
            if (projectData?.IndexImageData == null)
                return new NoContentResult();

            var bytes = Convert.FromBase64String(projectData.IndexImageData);
            return new FileStreamResult(new MemoryStream(bytes), "image/png");
        }

        /// <summary>
        /// 上传背景图
        /// </summary>
        [HttpPost("go-view-system/uploadBackGround")]
        public async Task<GoViewProjectUploadOutput> UploadBackGround(IFormFile @object)
        {
            // 文件名格式示例 13414795568325_index_preview.png
            var fileNameSplit = @object.FileName.Split('_');
            var idStr = fileNameSplit[0];
            if (!long.TryParse(idStr, out var id)) return new GoViewProjectUploadOutput();

            // 将预览图转换成 Base64
            var ms = new MemoryStream();
            await @object.CopyToAsync(ms);
            var base64Image = Convert.ToBase64String(ms.ToArray());

            // 保存
            if (await _goViewProjectDataRep.IsAnyAsync(u => u.Id == id))
            {
                await _goViewProjectDataRep.UpdateAsync
                    (u => new GoViewProjectDataEntity
                    {
                        BackGroundImageData = base64Image
                    }, u => u.Id == id);
            }
            else
            {
                await _goViewProjectDataRep.InsertAsync(new GoViewProjectDataEntity
                {
                    Id = id,
                    BackGroundImageData = base64Image,
                });
            }

            var output = new GoViewProjectUploadOutput
            {
                Id = id,
                BucketName = null,
                CreateTime = null,
                CreateUserId = null,
                FileName = null,
                FileSize = 0,
                FileSuffix = "png",
                FileUrl = $"api/app/go-view-system/getBackGroundImage/{id}",
                UpdateTime = null,
                UpdateUserId = null
            };

            return output;
        }

        /// <summary>
        /// 获取背景图
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("go-view-system/getBackGroundImage")]
        public async Task<IActionResult> GetBackGroundImage([FromQuery] long id)
        {
            var projectData = await _goViewProjectDataRep.GetByIdAsync(id);
            if (projectData?.BackGroundImageData == null)
                return new NoContentResult();

            var bytes = Convert.FromBase64String(projectData.BackGroundImageData);
            return new FileStreamResult(new MemoryStream(bytes), "image/png");
        }
    }
}
