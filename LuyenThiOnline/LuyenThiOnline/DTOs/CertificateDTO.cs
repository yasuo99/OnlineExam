using System.Collections.Generic;
using LuyenThiOnline.Models;

namespace LuyenThiOnline.DTOs
{
    public class CertificateDTO
    {
        public int Id { get; set; }
        public string CertificateName { get; set; } 
        public virtual IEnumerable<CertificateForAccountDetailDTO> Accounts{get;set;}
    }
}