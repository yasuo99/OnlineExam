using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LuyenThiOnline.Data;
using LuyenThiOnline.DTOs;
using LuyenThiOnline.Helpers;
using LuyenThiOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LuyenThiOnline.Controllers
{
    [Route("api/levels")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        private readonly ICRUDRepository _repo;
        private readonly IMapper _mapper;
        public LevelController(ICRUDRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(SubjectParams subjectParams)
        {
            var temp = await _repo.GetAll<Level>(subjectParams, "Accounts");
            var levelListForReturn = _mapper.Map<IEnumerable<LevelDetailDTO>>(temp);
            return Ok(levelListForReturn.ToList());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var level = await _repo.Detail<Level>(id, "Accounts");
            if (level == null)
            {
                return NotFound();
            }
            var levelReturn = _mapper.Map<LevelDetailDTO>(level);
            return Ok(levelReturn);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Level level)
        {
            Dictionary<dynamic,dynamic> properties = new Dictionary<dynamic, dynamic>();
            properties.Add("LevelName",level.LevelName);
            if(_repo.Exists<Level>(properties))
            {
                return BadRequest("Already Exist");
            }
            _repo.Create<Level>(level);
            if (await _repo.SaveAll())
            {
                return Ok();
            }
            return StatusCode(500);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Level level, int id)
        {
            // var levelFromDb = await _repo.Detail<Level>(id);
            // if (levelFromDb == null)
            // {
            //     return NotFound();
            // }
            // levelFromDb = level;
            var levelFromDb = await _repo.Detail<Level>(id);
            if(levelFromDb == null)
            {
                return NotFound();
            }
            levelFromDb.LevelName = level.LevelName;
            levelFromDb.StartExp = level.StartExp;
            levelFromDb.EndExp = level.EndExp;
            var updatedLevel = await _repo.Update<Level>(levelFromDb);
            if (await _repo.SaveAll())
            {
                return Ok(updatedLevel);
            }
            else
            {
                return StatusCode(500);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var levelFromDb = await _repo.Detail<Level>(id);
            if(levelFromDb == null)
            {
                return NotFound();
            }
             _repo.Delete<Level>(levelFromDb);
            if (await _repo.SaveAll())
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}