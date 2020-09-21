using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using LuyenThiOnline.Data;
using LuyenThiOnline.DTOs;
using LuyenThiOnline.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace LuyenThiOnline.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users/{userId}/photo")]
    public class PhotoController : ControllerBase
    {
        private readonly IOptions<CloudinarySetting> _cloudinaryConfig;
        private readonly ICRUDRepository _repo;
        private readonly IMapper _mapper;
        private Cloudinary _cloudinary;
        public PhotoController(ICRUDRepository repo, IMapper mapper, IOptions<CloudinarySetting> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _mapper = mapper;
            _repo = repo;
            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPhoto(int userId, [FromForm] PhotoDTO photo)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            var accountFromDb = await _repo.GetOneWithConditions<LuyenThiOnline.Models.Account>(account => account.Id == userId);
            var file = photo.File;
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var fileStream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, fileStream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            accountFromDb.PhotoUrl = uploadResult.Url.ToString();
            accountFromDb.PublicId = uploadResult.PublicId;
            if (await _repo.SaveAll())
            {
                return Ok(new
                {
                    photoUrl = accountFromDb.PhotoUrl
                });
            }
            return BadRequest("Error on uploading photo");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAvatar(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            var accountFromDb = await _repo.GetOneWithConditions<LuyenThiOnline.Models.Account>(account => account.Id == userId);
            if (accountFromDb.PublicId != null)
            {
                var deleteParams = new DeletionParams(accountFromDb.PublicId);
                var result = _cloudinary.Destroy(deleteParams);
                if (result.Result != "OK")
                {
                    return BadRequest("Api error");
                }
            }
            accountFromDb.PhotoUrl = null;
            accountFromDb.PublicId = null;
            if (await _repo.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Error on deleting photo");
        }
    }
}