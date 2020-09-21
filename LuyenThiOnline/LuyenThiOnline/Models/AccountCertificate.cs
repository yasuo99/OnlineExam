using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuyenThiOnline.Models
{
    public class AccountCertificate
    {
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        public int CertificateId { get; set; }
        [ForeignKey("CertificateId")]
        public virtual Certificate Certificate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}