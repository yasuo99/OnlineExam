using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuyenThiOnline.Models
{
    public class Rank
    {
        public int RankId { get; set; }
        public string RankName { get; set; }
        public virtual IEnumerable<Subject> Subjects { get; set; }
    }
}
