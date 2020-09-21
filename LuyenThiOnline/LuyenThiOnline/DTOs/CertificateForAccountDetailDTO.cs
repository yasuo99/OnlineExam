using System;
using LuyenThiOnline.Models;

namespace LuyenThiOnline.DTOs
{
    public class CertificateForAccountDetailDTO
    {
        public int CertificateId { get; set; }
        public string CertificateName { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}