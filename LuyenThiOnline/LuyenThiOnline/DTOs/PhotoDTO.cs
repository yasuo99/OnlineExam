using Microsoft.AspNetCore.Http;

namespace LuyenThiOnline.DTOs
{
    public class PhotoDTO
    {
        public string PhotoUrl { get; set; }
        public IFormFile File { get; set; }
        public string PublicId { get; set; }
    }
}