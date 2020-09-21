using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace LuyenThiOnline.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string PhotoUrl { get; set; }
        public string PublicId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt{get;set;}
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public int Exp { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }  
        public int LevelId { get; set; }
        [ForeignKey("LevelId")]
        public virtual Level Level { get; set; }
        public string RoleId { get; set; }  
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        public virtual IEnumerable<AccountBadge> Badges { get; set; }
        public virtual IEnumerable<AccountCertificate> Certificates{get;set;}
        public virtual IEnumerable<History> Histories { get; set; }
    }
}
