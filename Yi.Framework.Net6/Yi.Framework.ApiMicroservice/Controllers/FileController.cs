using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yi.Framework.Common.Const;
using Yi.Framework.Common.Enum;
using Yi.Framework.Common.Models;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.WebCore;

namespace Yi.Framework.ApiMicroservice.Controllers
{
    /// <summary>
    /// 文件
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private IFileService _iFileService;
        private readonly IHostEnvironment _env;

        /// <summary>
        /// 文件上传下载
        /// </summary>
        /// <param name="iFileService"></param>
        /// <param name="env"></param>
        public FileController(IFileService iFileService, IHostEnvironment env)
        {
            _iFileService = iFileService;
            _env = env;
        }

        /// <summary>
        /// 文件下载,只需用文件code即可
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [Route("/api/file/{code}")]
        [HttpGet]
        public async Task<IActionResult> Get(long code)
        {
            var file = await _iFileService._repository.GetByIdAsync(code);
            if (file is null)
            {
                return new NotFoundResult();
            }
            try
            {
                //路径为： 文件路径/文件id+文件扩展名
                var path = Path.Combine($"{PathConst.wwwroot}/{file.FilePath}", file.Id.ToString()+ Path.GetExtension(file.FileName));
                var stream = System.IO.File.OpenRead(path);
                var MimeType = Common.Helper.MimeHelper.GetMimeMapping(file.FileName);
                return  File(stream, MimeType, file.FileName);
            }
            catch
            {
                return new NotFoundResult();
            }
        }

        /// <summary>
        /// 多文件上传,type可空，默认上传至File文件夹下，swagger返回雪花id精度是有问题的
        /// </summary>
        /// <param name="type">文件类型，可空</param>
        /// <param name="file">多文件表单</param>
        /// <param name="remark">描述</param>
        /// <returns></returns>
        [Route("/api/file/Upload/{type?}")]
        [HttpPost]
        public async Task<Result> Upload([FromRoute] string? type, [FromForm] IFormFileCollection file,[FromQuery] string? remark)
        {
            if (type is null)
            {
                type = PathEnum.File.ToString();
            }
            else
            {
                type = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(type!.ToLower());
                if (!Enum.IsDefined(typeof(PathEnum), type))
                {
                    //后续类型可从字典表中获取
                    return Result.Error("上传失败！文件类型不支持！");
                }
            }

            if (file.Count() == 0)
            {
                return Result.Error("未选择文件");
            }
            //批量插入
            List<FileEntity> datas = new();

            //返回的codes
            List<long> codes = new();
            try
            {
                foreach (var f in file)
                {
                    FileEntity data = new();
                    data.Id = SnowFlakeSingle.Instance.NextId();
                    data.FileSize = ((decimal)f.Length) / 1024;
                    data.FileName = f.FileName;
                    data.FileType = Common.Helper.MimeHelper.GetMimeMapping(f.FileName);
                    data.FilePath = type;
                    data.Remark = remark;
                    data.IsDeleted = false;

                    //落盘文件，文件名为雪花id+自己的扩展名
                    string filename = data.Id.ToString() + Path.GetExtension(f.FileName);
                    using (var stream = new FileStream(Path.Combine($"{PathConst.wwwroot}/{type}", filename), FileMode.CreateNew, FileAccess.Write))
                    {
                        await f.CopyToAsync(stream);
                    }
                    //将文件信息添加到数据库
                    datas.Add(data);
                    codes.Add(data.Id);
                }
                return Result.Success().SetData(codes).SetStatus(await _iFileService._repository.InsertRangeAsync(datas));
            }
            catch
            {
                return Result.Error();
            }
        }
    }
}
