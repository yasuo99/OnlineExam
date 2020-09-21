using System.Collections.Generic;
using System.Threading.Tasks;
using LuyenThiOnline.Data;
using LuyenThiOnline.Helpers;
using LuyenThiOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuyenThiOnline.Controllers
{
    [Route("api/histories")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly ICRUDRepository _repo;
        public HistoryController(ICRUDRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(SubjectParams subjectParams){
            return Ok(await _repo.GetAll<History>(subjectParams, "Account,Subject"));
        }
        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetHistoryOfAccount(int accountId)
        {
            Dictionary<dynamic,dynamic> properties = new Dictionary<dynamic, dynamic>();
            properties.Add("AccountId",accountId);
            var histories = _repo.GetWithMultiPro<History>(properties);
            if(histories == null)
            {
                return NotFound();
            }
            return Ok(histories);
        }

    }
}