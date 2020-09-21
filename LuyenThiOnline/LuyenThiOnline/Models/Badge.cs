using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LuyenThiOnline.Models
{
    public class Badge
    {
        public int Id { get; set; }
        public string Badgename { get; set; }
        public string BadgeLogo { get; set; }
        public virtual IEnumerable<AccountBadge> Accounts { get; set; }
    }
}
