using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LuyenThiOnline.Models
{
    public class AccountBadge
    {
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        public int BadgeId { get; set; }   
        [ForeignKey("BadgeId")]
        public virtual Badge Badge { get; set; }
    }
}
