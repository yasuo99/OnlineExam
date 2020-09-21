using System;
using System.ComponentModel.DataAnnotations;

namespace LuyenThiOnline.DTOs
{
    public class AccountRegisterDTO
    {
        [Required,StringLength(10,MinimumLength = 6,ErrorMessage = "Username must between 6 and 10 characters")]
        public string Username { get; set; }
        [Required,StringLength(15,MinimumLength = 6,ErrorMessage = "Password must between 6 and 15 characters")]
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}