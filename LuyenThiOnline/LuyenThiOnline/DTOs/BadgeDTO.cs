using System.Collections.Generic;
using LuyenThiOnline.Models;

namespace LuyenThiOnline.DTOs
{
    public class BadgeDTO
    {
         public int Id { get; set; }
        public string Badgename { get; set; }
        public string BadgeLogo { get; set; }
        public virtual IEnumerable<AccountBadgeDTO> Accounts { get; set; }
    }
}