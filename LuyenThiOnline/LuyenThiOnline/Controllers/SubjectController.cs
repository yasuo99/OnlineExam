using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using LuyenThiOnline.Data;
using LuyenThiOnline.DTOs;
using LuyenThiOnline.Helpers;
using LuyenThiOnline.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuyenThiOnline.Controllers
{

    [Route("api/subjects")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ICRUDRepository _repo;
        private readonly IMapper _mapper;
        public SubjectController(ICRUDRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] SubjectParams subjectParams)
        {
            var subjects = await _repo.GetAll<Subject>(subjectParams);
            var returnSubjects = _mapper.Map<IEnumerable<SubjectDTO>>(subjects);
             
            {
                switch (subjectParams.OrderBy)
                {
                    case "az":
                        returnSubjects = returnSubjects.OrderBy(t => t.SubjectName);
                        break;
                    case "za":
                        returnSubjects = returnSubjects.OrderByDescending(t => t.SubjectName);
                        break;
                    case "enrolled":
                        returnSubjects = returnSubjects.OrderByDescending(t => t.EnrolledCount);
                        break;
                    case "exp":
                        returnSubjects = returnSubjects.OrderByDescending(t => t.ExpGain);
                        break;
                    default: 
                        break;
                }
            }
            Response.AddPaginationHeader(subjects.CurrentPages, subjects.PageSize, subjects.TotalCount, subjects.TotalPages);
            return Ok(returnSubjects);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var subject = await _repo.Detail<Subject>(id);
            if (subject == null)
            {
                return NotFound();
            }
            var returnSubject = _mapper.Map<SubjectDTO>(subject);
            return Ok(returnSubject);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Subject subject)
        {
            Dictionary<dynamic, dynamic> properties = new Dictionary<dynamic, dynamic>();
            properties.Add("SubjectName", subject.SubjectName);
            if (_repo.Exists<Subject>(properties))
            {
                return BadRequest("Already Exist");
            }
            _repo.Create<Subject>(subject);
            if (await _repo.SaveAll())
            {
                return Ok(subject);
            }
            return StatusCode(500);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Subject subject, int id)
        {
            var subjectFromDb = await _repo.Detail<Subject>(id);
            if (subjectFromDb == null)
            {
                return NotFound();
            }
            subjectFromDb.SubjectName = subject.SubjectName;
            subjectFromDb.RankId = subject.RankId;
            subjectFromDb.Description = subject.Description;
            subjectFromDb.ExpGain = subject.ExpGain;
            await _repo.Update<Subject>(subjectFromDb);
            if (await _repo.SaveAll())
            {
                return Ok(subjectFromDb);
            }
            return StatusCode(500);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var subjectFromDb = await _repo.Detail<Subject>(id);
            if (subjectFromDb == null)
            {
                return NotFound();
            }
            _repo.Delete(subjectFromDb);
            if (await _repo.SaveAll())
            {
                return Ok();
            }
            return StatusCode(500);
        }
        [Authorize]
        [HttpGet("exam/{subjectId}/do")]
        [ServiceFilter(typeof(LogUserActivity))]
        public async Task<IActionResult> DoExam(int subjectId, int pageSize, int page = 1)
        {
            var questionOfSubject = await _repo.GetWithConditions<Question>(u => u.SubjectId == subjectId);
            var returnQuestions = _mapper.Map<IEnumerable<QuestionForExamDTO>>(questionOfSubject);
            if (pageSize > 0)
            {
                returnQuestions = returnQuestions.Skip((page - 1) * pageSize).Take(pageSize);
            }
            return Ok(returnQuestions);
        }
        [Authorize]
        [HttpPost("{subjectId}/exam/{userId}/done")]
        public async Task<IActionResult> DoneExam(int userId, int subjectId, List<AnswerDTO> userAnswers)
        {
            var accountId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId != accountId)
            {
                return Unauthorized();
            }
            int score = 0;
            var questionAnswer = await _repo.GetWithConditions<Question>(u => u.SubjectId == subjectId, "Subject");
            foreach (var answer in userAnswers)
            {
                var question = questionAnswer.FirstOrDefault(u => u.Id == answer.Id);
                if (answer.Answer != null)
                {
                    if (answer.Answer.Equals(question.Answer))
                    {
                        answer.IsRightAnswer = true;
                        score += question.QuestionPoint;
                    }
                    else
                    {
                        answer.IsRightAnswer = false;
                    }
                }
                else
                {
                    answer.IsRightAnswer = false;
                    continue;
                }

            }
            var history = new History()
            {
                AccountId = accountId,
                SubjectId = subjectId,
                Score = score,
                DoneDate = DateTime.Now
            };
            if (score >= questionAnswer.Sum(questions => questions.QuestionPoint) * 0.75)
            {
                var accountFromDb = await _repo.GetOneWithConditions<Account>(account => account.Id == userId);
                accountFromDb.Exp += questionAnswer.FirstOrDefault().Subject.ExpGain;
            }
            var subject = await _repo.GetOneWithConditions<Subject>(subject => subject.Id == subjectId);
            subject.EnrolledCount++;
            _repo.Create<History>(history);
            await _repo.SaveAll();
            return Ok(new
            {
                score = score,
                result = userAnswers
            });
        }
        [HttpGet("questions/{questionId}")]
        public async Task<IActionResult> GetQuestion(int questionId)
        {
            var question = await _repo.GetOneWithConditions<Question>(question => question.Id == questionId);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }
        [HttpPost("{subjectId}/questions")]
        public async Task<IActionResult> CreateQuestion(Question question, int subjectId)
        {
            Dictionary<dynamic, dynamic> properties = new Dictionary<dynamic, dynamic>();
            properties.Add("QuestionContent", question.QuestionContent);
            if (_repo.Exists<Question>(properties))
            {
                return BadRequest("Already Exist");
            }
            _repo.Create(question);
            if (await _repo.SaveAll())
            {
                return Ok();
            }
            return StatusCode(500);
        }
        [HttpPut("{subjectId}/questions/{questionId}/edit")]
        public async Task<IActionResult> EditQuestion(Question question, int subjectId, int questionId)
        {
            var questionFromDb = await _repo.GetOneWithConditions<Question>(u => u.Id == questionId && u.SubjectId == subjectId);
            if (questionFromDb == null)
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
            await _repo.Update(questionFromDb);
            if (await _repo.SaveAll())
            {
                return Ok(questionFromDb);
            }
            return StatusCode(500);
        }
        [HttpDelete("{subjectId}/questions/remove/{questionId}")]
        public async Task<IActionResult> DeleteQuestion(int questionId, int subjectId)
        {
            var questionFromDb = await _repo.GetOneWithConditions<Question>(u => u.Id == questionId && u.SubjectId == subjectId);
            if (questionFromDb == null)
            {
                return NotFound();
            }
            _repo.Delete(questionFromDb);
            if (await _repo.SaveAll())
            {
                return Ok();
            }
            return StatusCode(500);
        }
    }
}