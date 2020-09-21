using System;
using System.Collections.Generic;
using LuyenThiOnline.Models;

namespace LuyenThiOnline.DTOs
{
    public class AccountForDetailDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string PhotoUrl { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public int Exp { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }  
        public string LevelName{get;set;}
        public string RoleName { get; set; }
        public virtual IEnumerable<AccountBadgeDTO> Badges { get; set; }
        public virtual IEnumerable<CertificateForAccountDetailDTO> Certificates { get; set; }
    }
}