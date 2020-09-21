using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuyenThiOnline.Data;
using LuyenThiOnline.Helpers;
using LuyenThiOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuyenThiOnline.Controllers
{
    [Route("api/certificates")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly ICRUDRepository _repo;
        public CertificateController(ICRUDRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(SubjectParams subjectParams){
            var certificates = await _repo.GetAll<Certificate>(subjectParams);
            return Ok(certificates);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var certificate = await _repo.Detail<Certificate>(id);
            if(certificate == null)
            {
                return NotFound();
            }
            return Ok(certificate);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Certificate certificate)
        {
            Dictionary<dynamic,dynamic> property = new Dictionary<dynamic, dynamic>();
            property.Add("CertificateName",certificate.CertificateName);
            if(_repo.Exists<Certificate>(property))
            {
                return BadRequest("Already Exists");
            }
            _repo.Create(certificate);
            if(await _repo.SaveAll())
            {
                return Ok(certificate);
            }
            return StatusCode(500);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Certificate certificate, int id)
        {
            var certificateFromDb = await _repo.Detail<Certificate>(id);
            if(certificateFromDb == null)
            {
                return NotFound();
            }
            certificateFromDb.CertificateName = certificate.CertificateName;
            await _repo.Update(certificateFromDb);
            if(await _repo.SaveAll())
            {
                return Ok(certificateFromDb);
            }
            return StatusCode(500);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var certificateFromDb = await _repo.Detail<Certificate>(id);
            if(certificateFromDb == null)
            {
                return NotFound();
            }
            _repo.Delete<Certificate>(certificateFromDb);
            if(await _repo.SaveAll()){
                return Ok();
            }
            return StatusCode(500);
        }
    }
}