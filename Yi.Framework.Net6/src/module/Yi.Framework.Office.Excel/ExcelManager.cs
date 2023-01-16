using OEM.Core;

namespace Yi.Framework.Office.Excel
{
    public class ExcelManager
    {
        private IExcelFactory _excelFactory;
        public ExcelManager(IExcelFactory excelFactory)
        {
            _excelFactory = excelFactory;
        }

        public List<T> ReadListByNameManager<T>(string path, string sheet) where T : class, new()
        {
            using (var excelAppService = _excelFactory.Create(System.IO.File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite)))
            {
                return excelAppService.ReadListByNameManager<T>(sheet);
            }
        }

        public T ReadByNameManager<T>(string path, string sheet) where T : class, new()
        {
            using (var excelAppService = _excelFactory.Create(System.IO.File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite)))
            {
                return excelAppService.ReadByNameManager<T>(sheet);
            }
        }

        public void WriteListByNameManager<T>(List<T> objcts, string sheet, string oldPath, string newPath) where T : class, new()
        {
            using (var excelAppService = _excelFactory.Create(System.IO.File.Open(oldPath, FileMode.OpenOrCreate, FileAccess.ReadWrite)))
            {
                excelAppService.WriteListByNameManager(objcts, sheet);
                excelAppService.Write(newPath);
            }
        }

        public void WriteByNameManager<T>(T objct, string sheet, string oldPath, string newPath) where T : class, new()
        {
            using (var excelAppService = _excelFactory.Create(System.IO.File.Open(oldPath, FileMode.OpenOrCreate, FileAccess.ReadWrite)))
            {
                excelAppService.WriteByNameManager(objct, sheet);
                excelAppService.Write(newPath);
            }
        }
    }
}