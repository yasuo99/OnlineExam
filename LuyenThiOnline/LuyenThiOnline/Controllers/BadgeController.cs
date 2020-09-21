using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LuyenThiOnline.Data;
using LuyenThiOnline.DTOs;
using LuyenThiOnline.Helpers;
using LuyenThiOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuyenThiOnline.Controllers
{
    [Route("api/badges")]
    [ApiController]
    public class BadgeController : ControllerBase
    {
        private readonly ICRUDRepository _repo;
        private readonly IMapper _mapper;
        public BadgeController(ICRUDRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(SubjectParams subjectParams){
            var badges = await _repo.GetAll<Badge>(subjectParams,"Accounts");
            return Ok(badges);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(){
            var badge = await _repo.Detail<Badge>("Accounts");
            if(badge == null)
            {
                return NotFound();
            }
            return Ok(badge);
        }
        [HttpPost]
        public async Task<IActionResult> Create(){
            return Ok();
        }
    }
}