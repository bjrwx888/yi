using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Org.BouncyCastle.Utilities.Collections;

namespace Yi.Framework.Module.Excel
{
    internal class OemClient
    {
        public void Export<T>(List<T> entityList, string filePath)
        {
            var properties = typeof(T).GetProperties().Where(x => x.GetCustomAttribute<DisplayNameAttribute>() is not null).Where(x => x.GetGetMethod().IsPublic).ToList();
            // 创建工作簿
            IWorkbook workbook = new XSSFWorkbook();
            // 创建工作表
            ISheet sheet = workbook.CreateSheet("sheet");


            // 写入表头
            IRow headerRow = sheet.CreateRow(0);
            for (int j = 0; j < properties.Count(); j++)
            {
                headerRow.CreateCell(j).SetCellValue(properties[j].GetCustomAttribute<DisplayNameAttribute>()!.DisplayName);
            }

            // 写入数据
            for (int i = 0; i < entityList.Count(); i++)
            {
                var currentEntity = entityList[i];
                IRow dataRow = sheet.CreateRow(i + 1);
                for (int j = 0; j < properties.Count(); j++)
                {
                    var currentPropertiy = properties[j];

                    //只处理简单类型
                    dataRow.CreateCell(j).SetCellValue(JsonSerializer.Serialize(currentPropertiy.GetValue(currentEntity)).TrimStart("\"".ToCharArray()).TrimEnd("\"".ToCharArray()));
                }
            }

            // 保存文件
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fs);
            }

            workbook.Dispose();
        }


        public List<T> Import<T>(string filePath) where T : new()
        {
            List<T> result = new();

            Dictionary<int, PropertyInfo> propHas = new Dictionary<int, PropertyInfo>();
            var properties = typeof(T).GetProperties().Where(x => x.GetCustomAttribute<DisplayNameAttribute>() is not null).Where(x => x.GetGetMethod().IsPublic).ToList();


            // 创建文件流
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // 创建工作簿
                IWorkbook workbook = new XSSFWorkbook(fileStream);

                // 选择第一个工作表
                ISheet sheet = workbook.GetSheetAt(0);

                //获取表头
                IRow headerRow = sheet.GetRow(0);


                for (int col = 0; col < headerRow.LastCellNum; col++)
                {
                    // 获取单元格的值
                    ICell cell = headerRow.GetCell(col);
                    var property = properties.Where(x => x.GetCustomAttribute<DisplayNameAttribute>().DisplayName == cell.StringCellValue).FirstOrDefault();
                    if (property is not null && !propHas.Values.Contains(property))
                    {
                        propHas[col] = property;
                    }
                }

                // 遍历行
                for (int row = 1; row <= sheet.LastRowNum; row++)
                {
                    //一行一个对象


                    IRow currentRow = sheet.GetRow(row);
                    var currentResult = new T();
                    // 遍历列
                    for (int col = 0; col < currentRow.LastCellNum; col++)
                    {
                        // 获取单元格的值
                        ICell cell = currentRow.GetCell(col);
                        string? cellValue = cell.ToString();
                        object value = cellValue.TrimStart("\"".ToCharArray()).TrimEnd("\"".ToCharArray());

                        value = Convert.ChangeType(cellValue, propHas[col].PropertyType);


                        propHas[col].SetValue(currentResult, value);

                    }

                    result.Add(currentResult);
                }
            }

            return result;
        }
    }
}
