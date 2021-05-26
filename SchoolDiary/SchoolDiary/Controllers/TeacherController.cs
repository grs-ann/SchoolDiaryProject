using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Domain.Models.Mark;
using SchoolDiary.Domain.Services;
using System;

namespace SchoolDiary.Controllers
{
    /// <summary>
    /// This controller provides possibility 
    /// for teacher to control study process management.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "teacher")]
    public class TeacherController : Controller
    {
        ITeacherService _teacherService;
        IMarkService _markService;
        public TeacherController(ITeacherService teacherService, IMarkService markService)
        {
            _teacherService = teacherService;
            _markService = markService;
        }
        /// <summary>
        /// Gets collection of classes, which 
        /// pinned to concrete teacher.
        /// </summary>
        /// <param name="teacherId">Teacher id in database table.</param>
        /// <returns>Result of query.</returns>
        [HttpGet(nameof(GetPinnedClasses))]
        public IActionResult GetPinnedClasses(int teacherId)
        {
            if (teacherId <= 0)
            {
                ModelState.AddModelError("Error", "Передано некорректное Id для учителя.");
            }
            if (ModelState.IsValid)
            {
                var pinnedClasses = _teacherService.GetPinnedClasses(teacherId);
                return Ok(pinnedClasses);
            }
            return BadRequest(ModelState);
        }
        [HttpGet(nameof(GetStudentMarks))]
        public IActionResult GetStudentMarks(int studentId, int subjectId, string firstDate, string lastDate)
        {
            if (!(studentId > 0 && subjectId > 0 && firstDate != null && lastDate != null))
            {
                ModelState.AddModelError("Error", "Ошибка валидации данных.");
            }
            DateTime firstDateObject;
            DateTime lastDateObject;
            var firstParsingResult = DateTime.TryParse(firstDate, out firstDateObject);
            var lastParsingResult = DateTime.TryParse(lastDate, out lastDateObject);
            if (!(firstParsingResult && lastParsingResult))
            {
                ModelState.AddModelError("Error", "Не удалось преобразовать строковые значения, пришедшие с клиента.");
            }
            if (ModelState.IsValid)
            {
                
                var studentMarks = _markService.GetConcreteStudentMarksBySubjectId(studentId, subjectId, firstDateObject, lastDateObject);
                if (studentMarks != null)
                {
                    return Ok(studentMarks);
                }
                ModelState.AddModelError("Error", "Не удалось получить оценки ученика.");
                return BadRequest(ModelState);
            }
            return BadRequest(ModelState);
        }
        [HttpGet(nameof(GetAllMarks))]
        public IActionResult GetAllMarks()
        {
            var marks = _markService.GetAllMarks();
            if (marks != null)
            {
                return Ok(marks);
            }
            ModelState.AddModelError("Error", "Не удалось получить данные об оценках из БД.");
            return BadRequest(ModelState);
        }
        [HttpPut(nameof(ChangeMark))]
        public IActionResult ChangeMark(MarkToChangeModel model)
        {
            if (ModelState.IsValid)
            {
                var changedMark = _markService.ChangeMark(model);
                if (changedMark != null)
                {
                    return Ok();
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost(nameof(AddNewMark))]
        public IActionResult AddNewMark(MarkToAddModel model)
        {
            if (ModelState.IsValid)
            {
                var addedMark = _markService.AddNewMarkAsync(model);
                if (addedMark.Result != null )
                {
                    return Ok(addedMark);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
