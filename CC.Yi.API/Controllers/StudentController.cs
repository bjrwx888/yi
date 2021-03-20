using CC.Yi.IBLL;
using CC.Yi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Yi.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private IstudentBll _studentBll;
        public StudentController(ILogger<StudentController> logger, IstudentBll studentBll)
        {
            _studentBll = studentBll;
            _logger = logger;
        }


        #region
        //下面，经典的 增删改查 即为简易--Yi意框架
        //注意：请确保你的数据库中存在合理的数据
        #endregion
        [HttpGet]
        public IActionResult GetTest()//查
        {
            var data = _studentBll.GetAllEntities().ToList();
            return Content(Common.JsonFactory.JsonToString(data));
        }
        [HttpGet]
        public IActionResult AddTest()//增
        {
           List<student> students = new List<student>() {new student { name = "学生a" } ,new student { name="学生d"} };
            _studentBll.Add(students);
           return Content("ok");

        }
        [HttpGet]
        public IActionResult RemoveTest()//删
        {
    
            if (_studentBll.Delete(u=>u.name=="学生a"))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }     
        }
        [HttpGet]
        public IActionResult UpdateTest()//改
        {
            if (_studentBll.Update(new student { id=2, name = "学生a" }, "name"))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }

        }
    }
}
