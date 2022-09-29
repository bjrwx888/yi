using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.Interface;
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
        private IUserService _iUserService;
        private readonly IHostEnvironment _env;
        /// <summary>
        /// 使用本地存储，未进行数据库记录
        /// </summary>
        /// <param name="iUserService"></param>
        /// <param name="env"></param>
        public FileController(IUserService iUserService, IHostEnvironment env)
        {
            _iUserService = iUserService;
            _env = env;
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [Route("/api/{type}/{fileName}")]
        [HttpGet]
        public IActionResult Get(string type, string fileName)
        {
            try
            {
                var path = Path.Combine($"wwwroot/{type}", fileName);
                var stream = System.IO.File.OpenRead(path);
                var MimeType = Common.Helper.MimeHelper.GetMimeMapping(fileName);
                return new FileStreamResult(stream, MimeType);
            }
            catch
            {
                return new NotFoundResult();
            }
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="type"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [Route("/api/Upload/{type}")]
        [HttpPost]
        public async Task<Result> Upload(string type, IFormFile file)
        {
            try
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                using (var stream = new FileStream(Path.Combine($"wwwroot/{type}", filename), FileMode.CreateNew, FileAccess.Write))
                {
                    await file.CopyToAsync(stream);
                }
                return Result.Success().SetData(filename);
            }
            catch
            {
                return Result.Error();
            }
        }
    }
}
