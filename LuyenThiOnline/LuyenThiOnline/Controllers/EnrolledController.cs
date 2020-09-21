using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuyenThiOnline.Data;
using LuyenThiOnline.Helpers;
using LuyenThiOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuyenThiOnline.Controllers
{
    [Route("api/enrolled")]
    [ApiController]
    public class EnrolledController : ControllerBase
    {
        private readonly ICRUDRepository _repo;
        public EnrolledController(ICRUDRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(SubjectParams subjectParams)
        {
            var enrolleds = await _repo.GetAll<Enrolled>(subjectParams, "Account,Subject");
            return Ok(enrolleds);
        }
        [HttpPost("enrolling")]
        public async Task<IActionResult> Enrolling(Enrolled enrolled)
        {
            Dictionary<dynamic, dynamic> properties = new Dictionary<dynamic, dynamic>();
            properties.Add("AccountId", enrolled.AccountId);
            properties.Add("SubjectId", enrolled.SubjectId);
            if (_repo.Exists<Enrolled>(properties))
            {
                return BadRequest("Already Enrolled");
            };
            _repo.Create<Enrolled>(enrolled);
            if (await _repo.SaveAll())
            {
                return Ok();
            }
            return StatusCode(500);
        }
        [HttpPut("done")]
        public async Task<IActionResult> Done(Enrolled enrolled)
        {
            Dictionary<dynamic,dynamic> properties = new Dictionary<dynamic, dynamic>();
            properties.Add("AccountId",enrolled.AccountId);
            properties.Add("SubjectId",enrolled.SubjectId);
            var enrolledFromDb = _repo.GetWithMultiPro<Enrolled>(properties);
            if(enrolledFromDb == null)
            {
                return NotFound();
            }
            enrolledFromDb.FirstOrDefault().IsPassed = true;
            await _repo.Update(enrolledFromDb.FirstOrDefault());
            if(await _repo.SaveAll())
            {
                return Ok(enrolledFromDb);
            }
            return StatusCode(500);
        }
        [HttpDelete("leaving")]
        public async Task<IActionResult> Leaving(Enrolled enrolled)
        {
            Dictionary<dynamic, dynamic> properties = new Dictionary<dynamic, dynamic>();
            properties.Add("AccountId", enrolled.AccountId);
            properties.Add("SubjectId", enrolled.SubjectId);
            if (!_repo.Exists<Enrolled>(properties))
            {
                return NotFound();
            };
            _repo.Delete(enrolled);
            if(await _repo.SaveAll())
            {
                return Ok();
            }
            return StatusCode(500);
        }   
    }
}
