﻿using System.IO.Compression;
using Volo.Abp.Application.Services;
using Yi.Framework.DigitalCollectibles.Domain.Entities;
using Yi.Framework.DigitalCollectibles.Domain.Shared.Consts;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.DigitalCollectibles.Application.Services.Tool;

public class CollectiblesToolService : ApplicationService
{
    private ISqlSugarRepository<CollectiblesAggregateRoot> _repository;

    public CollectiblesToolService(ISqlSugarRepository<CollectiblesAggregateRoot> repository)
    {
        _repository = repository;
    }

    public async Task<object> DeleteInitAsync()
    {
        var directoryPath = Path.Combine("wwwroot", "dc", "data");
        var dataPath = Path.Combine("wwwroot", "dc", "data", "data.zip");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        if (!File.Exists(dataPath))
        {
            throw new UserFriendlyException($"{dataPath}路径不存在数据");
        }

        ZipFile.ExtractToDirectory(dataPath, directoryPath, true); // true 表示覆盖已有文件


        var txtPath = Path.Combine("wwwroot", "dc","data", "data.txt");
        if (!File.Exists(txtPath))
        {
            throw new UserFriendlyException($"{txtPath}路径不存在文本");
        }

        var lines = await File.ReadAllLinesAsync(txtPath);
        var errData = new List<string>();
        //自动开启事务
        List<CollectiblesAggregateRoot> entities = new List<CollectiblesAggregateRoot>();
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            var items = line.Split(",").ToList();


            if (items.Count!=6)
            {
                errData.Add($"{i}行数据存，数据不对，只能6个数据");
            }
            
            var fileName = items[0];
            var code = Path.GetFileNameWithoutExtension(fileName) ;
            var name = items[1];
            var value = decimal.Parse(items[2]);
            var url = $"https://ccnetcore.com/prod-api/wwwroot/dc/data/{fileName}";
            var rarity = int.Parse(items[3]);
            var des = items[4];
            var find = int.Parse(items[5]);

            var entity = new CollectiblesAggregateRoot
            {
                Code = code,
                Name = name,
                Describe = des,
                ValueNumber = value,
                Url = url,
                Rarity = (RarityEnum)Enum.ToObject(typeof(RarityEnum), rarity),
                FindTotal = find,
                OrderNum = 0
            };
            entities.Add(entity);

            if (!File.Exists(Path.Combine(directoryPath,fileName)))
            {
                errData.Add($"文件不存在：{Path.Combine(directoryPath,fileName)}");
            }
            
            
        }

        if (errData.Any())
        {
            return new { ok = false, errData };
        }
        
        var allCode = await _repository._DbQueryable.Select(x => x.Code).ToListAsync();

        var existCodes = allCode.Intersect(entities.Select(x => x.Code)).ToList();
        if (existCodes.Count > 0)
        {
            return new { ok = false, existCodes };
        }
        

        await _repository.InsertRangeAsync(entities);
        return new { ok = true,entities };
    }
}