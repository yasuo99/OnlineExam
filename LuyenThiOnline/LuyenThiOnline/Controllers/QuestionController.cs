using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuyenThiOnline.Data;
using LuyenThiOnline.Helpers;
using LuyenThiOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuyenThiOnline.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ICRUDRepository _repo;
        public QuestionController(ICRUDRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(SubjectParams subjectParams){
            var questions = await _repo.GetAll<Question>(subjectParams, "Subject");
            return Ok(questions);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var questionFromDb = await _repo.Detail<Question>("Subject");
            if(questionFromDb == null)
            {
                return NotFound();
            }
            return Ok(questionFromDb);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Question question)
        {
            Dictionary<dynamic,dynamic> properties = new Dictionary<dynamic, dynamic>();
            properties.Add("QuestionContent",question.QuestionContent);
            if(_repo.Exists<Question>(properties))
            {
                return BadRequest("Already Exist");
            }
            _repo.Create(question);
            if(await _repo.SaveAll())
            {
                return Ok();
            }
            return StatusCode(500);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Question question,int id)
        {
            var questionFromDb = await _repo.Detail<Question>(id);
            if(questionFromDb == null)
            {
                return NotFound();
            }
            questionFromDb.QuestionContent = question.QuestionContent;
            questionFromDb.A = question.A;
            questionFromDb.B = question.B;
            questionFromDb.C = question.C;
            questionFromDb.D = question.D;
            questionFromDb.Answer = question.Answer;
            questionFromDb.SubjectId = question.SubjectId;
            await _repo.Update<Question>(questionFromDb);
            if(await _repo.SaveAll())
            {
                return Ok(questionFromDb);
            }
            return StatusCode(500);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var questionFromDb = await _repo.Detail<Question>(id);
            if(questionFromDb == null)
            {
                return NotFound();
            }
            _repo.Delete<Question>(questionFromDb);
            if(await _repo.SaveAll())
            {
                return Ok();
            }
            return StatusCode(500);
        }
    }
}