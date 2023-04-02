﻿using Cike.AutoWebApi.Setting;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.AspNetCore.Extensions;
using Yi.Framework.Core.Const;
using Yi.Framework.Core.Enums;
using Yi.Framework.Core.Helper;
using Yi.Framework.Ddd.Repositories;
using Yi.Framework.Ddd.Services;
using Yi.Framework.Ddd.Services.Abstract;
using Yi.Framework.ImageSharp;

namespace Yi.Framework.FileManager
{
    /// <summary>
    /// 文件处理
    /// </summary>
    public class FileService : ApplicationService, IFileService, IAutoApiService
    {
        private readonly IRepository<FileEntity> _repository;
        private readonly ImageSharpManager _imageSharpManager;
        private readonly HttpContext _httpContext;
        public FileService(IRepository<FileEntity> repository, ImageSharpManager imageSharpManager, IHttpContextAccessor httpContextAccessor
           )
        {
            _repository = repository;
            _imageSharpManager = imageSharpManager;
            if (httpContextAccessor.HttpContext is null)
            {
                throw new ApplicationException("HttpContext为空");
            }
            _httpContext = httpContextAccessor.HttpContext;
        }

        /// <summary>
        /// 下载文件,是否缩略图
        /// </summary>
        /// <returns></returns>
        [Route("/api/file/{code}/{isThumbnail?}")]
        public async Task<IActionResult> Get([FromRoute] long code, [FromRoute] bool? isThumbnail)
        {
            var file = await _repository.GetByIdAsync(code);
            if (file is null)
            {
                return new NotFoundResult();
            }

            var path = file.FilePath;
            //如果为缩略图，需要修改路径
            if (isThumbnail is true)
            {
                path = $"{PathConst.wwwroot}/{FileTypeEnum.Thumbnail}/{file.Id}{Path.GetExtension(file.FileName)}";
            }
            //路径为： 文件路径/文件id+文件扩展名

            if (!File.Exists(path))
            {
                return new NotFoundResult();
            }

            var steam = await File.ReadAllBytesAsync(path);

            //设置附件下载，下载名称
            _httpContext.FileAttachmentHandle(file.FileName);
            return new FileContentResult(steam, file.FileContentType ?? @"text/plain");
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public async Task<List<FileGetListOutputDto>> Post([FromForm] IFormFileCollection file)
        {
            if (file.Count() == 0)
            {
                throw new ArgumentException("文件上传为空！");
            }
            //批量插入
            List<FileEntity> entities = new();

            foreach (var f in file)
            {
                FileEntity data = new();
                data.Id = SnowflakeHelper.NextId;
                data.FileSize = ((decimal)f.Length) / 1024;
                data.FileName = f.FileName;


                data.FileContentType = MimeHelper.GetMimeMapping(f.FileName);


                var type = MimeHelper.GetFileType(f.FileName);

                //落盘文件，文件名为雪花id+自己的扩展名
                string filename = data.Id.ToString() + Path.GetExtension(f.FileName);
                string typePath = $"{PathConst.wwwroot}/{type}";
                if (!Directory.Exists(typePath))
                {
                    Directory.CreateDirectory(typePath);
                }

                var filePath = Path.Combine(typePath, filename);
                data.FilePath = filePath;


                //生成文件
                using (var stream = new FileStream(filePath, FileMode.CreateNew, FileAccess.ReadWrite))
                {
                    await f.CopyToAsync(stream);

                    //如果是图片类型，还需要生成缩略图,当然，如果图片很小，直接复制过去即可
                    if (FileTypeEnum.Image.Equals(type))
                    {
                        string thumbnailPath = $"{PathConst.wwwroot}/{FileTypeEnum.Thumbnail}";
                        if (!Directory.Exists(thumbnailPath))
                        {
                            Directory.CreateDirectory(thumbnailPath);
                        }
                        string thumbnailFilePath = Path.Combine(thumbnailPath, filename);
                        try
                        {
                            _imageSharpManager.ImageCompress(f.FileName, f.OpenReadStream(), thumbnailFilePath);
                        }
                        catch
                        {
                            var result = new byte[stream.Length];
                            await stream.ReadAsync(result, 0, result.Length);
                            await File.WriteAllBytesAsync(thumbnailFilePath, result);
                        }
                    }


                };
                entities.Add(data);
            }
            await _repository.InsertRangeAsync(entities);
            return entities.Adapt<List<FileGetListOutputDto>>();


        }
    }
}
