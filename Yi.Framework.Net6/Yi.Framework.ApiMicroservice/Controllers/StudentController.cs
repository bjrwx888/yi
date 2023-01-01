
using Microsoft.AspNetCore.Mvc;
using Yi.Framework.Common.Models;
using Yi.Framework.DTOModel.RABC.Student;
using Yi.Framework.Interface.RABC;

namespace Brick.IFServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {

        private readonly ILogger<StudentController> _logger;
        private readonly IStudentService _studentService;
        public StudentController(ILogger<StudentController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        [HttpDelete]
        [Route("ErrorTest")]
        public Result<bool> ErrorTest()
        {
            _studentService.GetError();
            return Result<bool>.Success();
        }

        /// <summary>
        /// 增
        /// </summary>
        /// <param name="studentCreateInput"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<StudentGetOutput>> Create(StudentCreateInput studentCreateInput)
        {
            var result = await _studentService.CreateAsync(studentCreateInput);

            return Result<StudentGetOutput>.Success().SetData(result);

        }

        /// <summary>
        /// 查
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<Result<StudentGetOutput>> GetById(Guid id)
        {
            var result = await _studentService.GetByIdAsync(id);
            return Result<StudentGetOutput>.Success().SetData(result);
        }

        /// <summary>
        /// 查
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result<List<StudentListOutput>>> GetLsit()
        {
            var result = await _studentService.GetListAsync();

            return Result<List<StudentListOutput>>.Success().SetData(result);
        }

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<Result<bool>> Del(List<Guid> ids)
        {
            await _studentService.DeleteAsync(ids);
            return Result<bool>.Success();
        }

        /// <summary>
        /// 更
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentUpdateInput"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<Result<StudentGetOutput>> Update(Guid id, StudentUpdateInput studentUpdateInput)
        {
            var result = await _studentService.UpdateAsync(id, studentUpdateInput);
            return Result<StudentGetOutput>.Success().SetData(result);
        }


    }
}