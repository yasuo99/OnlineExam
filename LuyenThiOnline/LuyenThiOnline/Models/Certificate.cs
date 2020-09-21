using System;
using System.Collections.Generic;

namespace LuyenThiOnline.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public string CertificateName { get; set; } 
        public virtual IEnumerable<AccountCertificate> Accounts{get;set;}
    }
}