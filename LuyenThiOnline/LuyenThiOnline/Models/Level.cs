using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LuyenThiOnline.Models
{
    public class Level
    {
        public int Id { get; set; }
        public string LevelName { get; set; }
        public int StartExp { get; set; }
        public int EndExp { get; set; }
        public virtual IEnumerable<Account> Accounts { get; set; }
    }
}
