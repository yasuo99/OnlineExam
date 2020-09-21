using System;

namespace LuyenThiOnline.DTOs
{
    public class AccountForLevelListDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string PhotoUrl { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public int Exp { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }  

    }
}