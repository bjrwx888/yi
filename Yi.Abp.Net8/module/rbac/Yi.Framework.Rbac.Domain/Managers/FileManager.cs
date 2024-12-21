using Mapster;
using Microsoft.AspNetCore.Http;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Yi.Framework.Core.Enums;
using Yi.Framework.Core.Helper;
using Yi.Framework.Rbac.Domain.Entities;

namespace Yi.Framework.Rbac.Domain.Managers;

public class FileManager : DomainService, IFileManager
{
    private IGuidGenerator _guidGenerator;
    private readonly IRepository<FileAggregateRoot> _repository;

    public FileManager(IGuidGenerator guidGenerator, IRepository<FileAggregateRoot> repository)
    {
        _guidGenerator = guidGenerator;
        _repository = repository;
    }

    /// <summary>
    /// 批量插入数据库
    /// </summary>
    /// <param name="files"></param>
    /// <exception cref="ArgumentException"></exception>
    public async Task<List<FileAggregateRoot>> CreateAsync(IEnumerable<IFormFile> files)
    {
        if (files.Count() == 0)
        {
            throw new ArgumentException("文件上传为空！");
        }

        //批量插入
        List<FileAggregateRoot> entities = new();
        foreach (var file in files)
        {
            FileAggregateRoot data = new(_guidGenerator.Create(), file.FileName, (decimal)file.Length / 1024);
            data.CheckDirectoryOrCreate();
            entities.Add(data);
        }

        await _repository.InsertManyAsync(entities);
        return entities;
    }


    /// <summary>
    /// 保存文件
    /// </summary>
    /// <param name="file"></param>
    /// <param name="fileStream"></param>
    public async Task SaveFileAsync(FileAggregateRoot file,Stream fileStream)
    {
        var filePath = file.GetSaveFilePath();

        //生成文件
        using (var stream = new FileStream(filePath, FileMode.CreateNew, FileAccess.ReadWrite))
        {
            await fileStream.CopyToAsync(stream);

            //如果是图片类型，还需要生成缩略图
            //这里根据自己需求变更，我们的需求是：原始文件与缩略图文件，都要一份
            var fileType=file.GetFileType();;
            //如果文件类型是图片，尝试进行压缩
            if (FileTypeEnum.Image.Equals(fileType))
            {
               var thumbnailSavePath= file.GetAndCheakThumbnailSavePath(true);
                try
                {
                    // _imageSharpManager.ImageCompress(f.FileName, f.OpenReadStream(), thumbnailFilePath);
                }
                catch
                {
                    //如果失败了，直接复制一份到缩略图上即可
                    var result = new byte[stream.Length];
                    await stream.ReadAsync(result, 0, result.Length);
                    await File.WriteAllBytesAsync(thumbnailSavePath, result);
                }
            }
        }
    }
}