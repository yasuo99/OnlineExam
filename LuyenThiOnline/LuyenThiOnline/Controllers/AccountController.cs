using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LuyenThiOnline.Data;
using LuyenThiOnline.DTOs;
using LuyenThiOnline.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using LuyenThiOnline.Helpers;

namespace LuyenThiOnline.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ICRUDRepository _repository;
        public AccountController(IAuthRepository repo, IConfiguration config, IMapper mapper, ICRUDRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
            _config = config;
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // var token = this.HttpContext.Request.Headers.Where(u => u.Key == "Authorization");
            // return Ok(token);
            int userId = -1;
            if(User.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            var users = await _repo.GetAll();
            users = users.Where(user => user.Id != userId);
            var returnUsers = _mapper.Map<IEnumerable<AccountForDetailDTO>>(users);
            return Ok(returnUsers);
        }
        [Authorize]
        [HttpGet("accountdetail/{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var account = await _repo.GetUser(id);
            if (account == null)
            {
                return NotFound();
            }
            var accountForReturn = _mapper.Map<AccountForDetailDTO>(account);
            return Ok(accountForReturn);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountRegisterDTO accountRegisterDTO)
        {
            accountRegisterDTO.Username = accountRegisterDTO.Username.ToLower();
            if (await _repo.Exists(accountRegisterDTO.Username))
            {
                return BadRequest("Username already used");
            }
            // var account = new Account()
            // {
            //     Username = accountRegisterDTO.Username,
            //     PhoneNumber = accountRegisterDTO.PhoneNumber,
            //     DateOfBirth = accountRegisterDTO.DateOfBirth,
            //     Created = DateTime.Now,
            //     Gender = accountRegisterDTO.Gender,
            //     LevelId = 1,
            //     Exp = 0
            // };
            var account = _mapper.Map<Account>(accountRegisterDTO);
            account.LevelId = 1;
            account.Exp = 0;
            var createdAccount = await _repo.Register(account, accountRegisterDTO.Password);
            if (createdAccount == null)
            {
                return null;
            }
            var accountReturn = _mapper.Map<AccountForDetailDTO>(account);
            return CreatedAtRoute("GetUser", new {controller = "Account", id = account.Id}, accountReturn);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(AccountLoginDTO accountLoginDTO)
        {
            var loginAccount = await _repo.Login(accountLoginDTO.Username, accountLoginDTO.Password);
            if (loginAccount == null)
            {
                return Unauthorized();
            }
            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier,loginAccount.Id.ToString()),
                new Claim(ClaimTypes.Name,loginAccount.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var returnUser = _mapper.Map<AccountForDetailDTO>(loginAccount);
            return Ok(new
            {
                tokenString = tokenHandler.WriteToken(token),
                user = returnUser
            });
        }
        [HttpPut("editaccount/{id}")]
        public async Task<IActionResult> UpdateAccountInfo(int id, AccountForUpdateDTO account)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            var accountFromDb = await _repo.GetUser(id);
            if (accountFromDb == null)
            {
                return NotFound();
            }
            _mapper.Map(accountFromDb, account);
            if (await _repo.SaveAll())
            {
                return NoContent();
            }
            throw new Exception("Error on updating account information");
        }
        [HttpPut("changepw/{id}")]
        public async Task<IActionResult> ChangePassword(int id, AccountForChangePWDTO account)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == null)
            {
                return Unauthorized();
            }
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            var accountFromDb = await _repo.GetUser(id);
            if (accountFromDb == null)
            {
                return NotFound();
            }
            var updatedAccount = await _repo.ChangePassword(account);
            if (updatedAccount == null)
            {
                return BadRequest("Wrong old password");
            }
            if (await _repo.SaveAll())
            {
                return NoContent();
            }
            throw new Exception($"Error on updating account {id} password");
        }
        [Authorize]
        [HttpGet("{id}/histories")]
        public async Task<IActionResult> GetAccountHistories(int id)
        {
            var histories = await _repository.GetWithConditions<History>(account => account.AccountId == id,"Account,Subject");
            var returnHistories = _mapper.Map<IEnumerable<HistoryDTO>>(histories);
            return Ok(returnHistories);
        }
    }
}
